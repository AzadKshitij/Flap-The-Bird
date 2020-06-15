using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Bird : MonoBehaviour
{
  private static Bird instance;
  public event EventHandler OnDied;
  public event EventHandler OnStartPlaying;
  // public Text jumpText;
  private const float JUMP_AMOUNT = 15f;
  private Rigidbody2D birdRigidbody2D;
  private State state;
  public GameObject jumpObject;


  private enum State
  {
    WaitingToStart,
    Playing,
    Dead
  }

  public static Bird GetInstance()
  {
    return instance;
  }
  private void Awake()
  {
    instance = this;
    birdRigidbody2D = GetComponent<Rigidbody2D>();
    birdRigidbody2D.bodyType = RigidbodyType2D.Static;
    state = State.WaitingToStart;
  }
  private void Update()
  {
    switch (state)
    {
      default:
      case State.WaitingToStart:
        if (Input.GetMouseButtonDown(0))
        {
          state = State.Playing;
          birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
          Jump();
          if (OnStartPlaying != null) OnStartPlaying(this, EventArgs.Empty);
        }
        break;
      case State.Playing:
        if (Input.GetMouseButtonDown(0))
        {
          Jump();
        }
        break;
      case State.Dead:
        break;
    }
  }

  private void Jump()
  {
    jumpObject.SetActive(false);
    birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
    SoundManager.PlaySound(SoundManager.Sound.BirdJump);
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    // CMDebug.TextPopupMouse("Dead!");
    birdRigidbody2D.bodyType = RigidbodyType2D.Static;
    SoundManager.PlaySound(SoundManager.Sound.Lose);
    if (OnDied != null) OnDied(this, EventArgs.Empty);

  }
}
