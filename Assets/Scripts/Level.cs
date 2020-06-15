using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey;
using CodeMonkey.Utils;

public class Level : MonoBehaviour
{
  private const float CAMERA_ORTHO_SIZE = 6f;
  private const float PIPE_WIDTH = 10.4f;
  //   private const float Head_height = 1.875f;
  private const float PIPE_MOVE_SPEED = 3.2f;

  private const float PIPE_DESTROY_X_POSITION = -8f;
  private const float PIPE_SPAWN_X_POSITION = 7f;
  private const float GROUND_DESTROY_X_POSITION = -11f;
  private const float CLOUD_DESTROY_X_POSITION = -11f;
  private const float GROUND_SPAWN_X_POSITION = 7f;
  private const float CLOUD_SPAWN_X_POSITION = +11f;
  private const float CLOUD_SPAWN_Y_POSITION = +5f;

  private const float BIRD_X_POSITION = 0f;
  private int pipeSpawned;

  private static Level instance;

  public static Level GetInstance()
  {
    return instance;
  }
  private List<Transform> groundList;
  private List<Transform> cloudList;
  private List<Pipe> pipeList;
  private int pipePassedCount;

  private float pipeSpawnTimer;
  private float cloudSpawnTimer;
  private float pipeSpawnTimerMax;
  private float gapSize;
  private State state;

  public enum Difficulty
  {
    Easy,
    Medium,
    Hard,
    Impossible,
  }

  private enum State
  {
    WaitingToStart,
    Playing,
    BiedDead,
  }

  private void Awake()
  {
    instance = this;
    SpawnInitialGround();
    SpawnInitialClouds();
    pipeList = new List<Pipe>();
    pipeSpawnTimerMax = 2f;
    SetDifficulty(Difficulty.Easy);
    state = State.WaitingToStart;
  }

  private void Start()
  {
    // CreateGapPipes(6f, 1f, 1f);
    Bird.GetInstance().OnDied += Bird_OnDied;
    Bird.GetInstance().OnStartPlaying += Bird_OnStartPlaying;
    Score.Start();
  }

  private void Bird_OnStartPlaying(object sender, System.EventArgs e)
  {
    state = State.Playing;
  }

  private void Bird_OnDied(object sender, System.EventArgs e)
  {
    state = State.BiedDead;
  }
  private void Update()
  {
    if (state == State.Playing)
    {
      HandlePipeMovement();
      HandlePipeSpawn();
      HandleGround();
      HandleClouds();
    }

  }

  private void SpawnInitialClouds()
  {
    cloudList = new List<Transform>();
    Transform cloudTransform;
    cloudTransform = Instantiate(GetCloudPrefabTransform(), new Vector3(0, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
    cloudList.Add(cloudTransform);
  }

  private Transform GetCloudPrefabTransform()
  {
    switch (Random.Range(0, 3))
    {
      default:
      case 1: return gameAssets.GetInstance().pfCloud_2;
      case 2: return gameAssets.GetInstance().pfCloud_3;
      case 0: return gameAssets.GetInstance().pfCloud_1;
    }
  }

  private void HandleClouds()
  {
    // Handle Cloud Spawning
    cloudSpawnTimer -= Time.deltaTime;
    if (cloudSpawnTimer < 0)
    {
      // Time to spawn another cloud
      float cloudSpawnTimerMax = 4.4f;
      cloudSpawnTimer = cloudSpawnTimerMax;
      Transform cloudTransform = Instantiate(GetCloudPrefabTransform(), new Vector3(CLOUD_SPAWN_X_POSITION, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
      cloudList.Add(cloudTransform);
    }

    // Handle Cloud Moving
    for (int i = 0; i < cloudList.Count; i++)
    {
      Transform cloudTransform = cloudList[i];
      // Move cloud by less speed than pipes for Parallax
      cloudTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime * .7f;

      if (cloudTransform.position.x < CLOUD_DESTROY_X_POSITION)
      {
        // Cloud past destroy point, destroy self
        Destroy(cloudTransform.gameObject);
        cloudList.RemoveAt(i);
        i--;
      }
    }
  }

  private void SpawnInitialGround()
  {
    groundList = new List<Transform>();
    Transform groundTransform;
    float groundY = -5.78f;
    float groundWidth = 19.1f;
    groundTransform = Instantiate(gameAssets.GetInstance().pfGround, new Vector3(0, groundY, 0), Quaternion.identity);
    groundList.Add(groundTransform);
    groundTransform = Instantiate(gameAssets.GetInstance().pfGround, new Vector3(groundWidth, groundY, 0), Quaternion.identity);
    groundList.Add(groundTransform);
    groundTransform = Instantiate(gameAssets.GetInstance().pfGround, new Vector3(groundWidth * 2f, groundY, 0), Quaternion.identity);
    groundList.Add(groundTransform);
  }

  private void HandleGround()
  {
    foreach (Transform groundTransform in groundList)
    {
      groundTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;

      if (groundTransform.position.x < GROUND_DESTROY_X_POSITION)
      {
        // Ground passed the left side, relocate on right side
        // Find right most X position
        float rightMostXPosition = -9f;
        for (int i = 0; i < groundList.Count; i++)
        {
          if (groundList[i].position.x > rightMostXPosition)
          {
            rightMostXPosition = groundList[i].position.x;
          }
        }

        // Place Ground on the right most position
        float groundWidth = 19.1f;
        groundTransform.position = new Vector3(rightMostXPosition + groundWidth, groundTransform.position.y, groundTransform.position.z);
      }
    }
  }

  private void HandlePipeSpawn()
  {
    pipeSpawnTimer -= Time.deltaTime;
    if (pipeSpawnTimer < 0)
    {
      //   it's time to spawn another pipe
      pipeSpawnTimer += pipeSpawnTimerMax;
      float heightEdgeLimit = 0.7f;
      float minHeight = gapSize * 0.5f + heightEdgeLimit;
      float maxHeight = CAMERA_ORTHO_SIZE * 2f - minHeight;
      float height = Random.Range(minHeight, maxHeight);
      CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
    }
  }

  private void HandlePipeMovement()
  {
    for (int i = 0; i < pipeList.Count; i++)
    {
      Pipe pipe = pipeList[i];
      bool isToTheRightOfBird = pipe.GetXPosition() > BIRD_X_POSITION;
      pipe.Move();

      if (isToTheRightOfBird && pipe.GetXPosition() <= BIRD_X_POSITION)
      {
        // Pipe Passed Bird
        pipePassedCount++;
        SoundManager.PlaySound(SoundManager.Sound.Score);
      }


      if (pipe.GetXPosition() < PIPE_DESTROY_X_POSITION)
      {
        // destroy the pipe
        pipe.DestroySelf();
        //removeing from the list 
        pipeList.Remove(pipe);
        i--;
      }
    }
  }

  private void SetDifficulty(Difficulty difficulty)
  {
    switch (difficulty)
    {
      case Difficulty.Easy:
        gapSize = 6.8f;
        pipeSpawnTimerMax = 2.1f;
        break;
      case Difficulty.Medium:
        gapSize = 5.9f;
        pipeSpawnTimerMax = 2f;
        break;
      case Difficulty.Hard:
        gapSize = 4.6f;
        pipeSpawnTimerMax = 1.5f;
        break;
      case Difficulty.Impossible:
        gapSize = 3.9f;
        pipeSpawnTimerMax = 1.2f;
        break;
    }
  }
  private Difficulty GetDifficulty()
  {
    if (pipeSpawned > 21) return Difficulty.Impossible;
    if (pipeSpawned > 14) return Difficulty.Hard;
    if (pipeSpawned > 7) return Difficulty.Medium;
    return Difficulty.Easy;
  }
  private void CreateGapPipes(float gapY, float gapSize, float xPosition)
  {
    CreatePipe(gapY - gapSize * 0.5f, xPosition, true);
    CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * 0.5f, xPosition, false);
    pipeSpawned++;
    SetDifficulty(GetDifficulty());
  }

  private void CreatePipe(float height, float xPosition, bool createBottom)
  {
    //   HashSet up head
    Transform pipeHead = Instantiate(gameAssets.GetInstance().pfPipeHead);
    float pipeHeadYPosition;
    if (createBottom) { pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - 0.205f; } else { pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + 0.205f; }
    pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);

    // pipeList.Add(pipeHead);

    // Set up body
    Transform pipeBody = Instantiate(gameAssets.GetInstance().pfPipeBody);
    float pipeBodyYPosition;
    if (createBottom) { pipeBodyYPosition = -CAMERA_ORTHO_SIZE; }
    else
    {
      pipeBodyYPosition = +CAMERA_ORTHO_SIZE;
      pipeBody.localScale = new Vector3(0.078f, -1f, 1f);
    }
    pipeBody.position = new Vector3(xPosition, pipeBodyYPosition);

    // pipeList.Add(pipeBody);

    SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();

    pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

    BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
    pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
    pipeBodyBoxCollider.offset = new Vector2(0, height * 0.5f);

    Pipe pipe = new Pipe(pipeHead, pipeBody);

    pipeList.Add(pipe);
  }

  public int GetPipePassed()
  {
    return (pipePassedCount / 2);
  }


  /*----------- representating a Single Pipe ---------*/
  private class Pipe
  {
    private Transform pipeHeadTransform;
    private Transform pipeBodyTransform;

    private bool isBottom;

    public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform)
    {
      this.pipeBodyTransform = pipeBodyTransform;
      this.pipeHeadTransform = pipeHeadTransform;
    }

    public void Move()
    {
      pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
      pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
    }

    public float GetXPosition()
    {
      return pipeHeadTransform.position.x;
    }

    public void DestroySelf()
    {
      Destroy(pipeHeadTransform.gameObject);
      Destroy(pipeBodyTransform.gameObject);
    }



  }
}
