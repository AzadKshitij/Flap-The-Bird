using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class GameOverWindow : MonoBehaviour
{
  private Text scoreText;
  private Text highscoreText;
  private Text Gold;
  public GameObject inGameScore;
  public GameObject inGameGold;
  private void Awake()
  {
    scoreText = transform.Find("scoreText").GetComponent<Text>();
    highscoreText = transform.Find("highscoreText").GetComponent<Text>();
    Gold = transform.Find("Gold").GetComponent<Text>();

    transform.Find("RetryButton").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.Game); };
    transform.Find("RetryButton").GetComponent<Button_UI>().AddButtonSounds();


    transform.Find("MainMenuBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.MainMenuScene); };
    transform.Find("MainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();
  }

  private void Start()
  {
    Bird.GetInstance().OnDied += Bird_OnDiad;
    Hide();
  }

  private void Bird_OnDiad(object sender, System.EventArgs e)
  {
    VideoAds.GetInstance().DisplayVideoAd();
    GoldHandeler.GetInstance().setCoins(Level.GetInstance().GetPipePassed());
    scoreText.text = Level.GetInstance().GetPipePassed().ToString();
    Gold.text = (Level.GetInstance().GetPipePassed() / 5).ToString();
    inGameScore.SetActive(false);
    inGameGold.SetActive(false);
    if (Level.GetInstance().GetPipePassed() >= Score.GetHighScore())
    {
      //new highscore!
      highscoreText.text = "NEW HIGHSCORE";
    }
    else
    {
      highscoreText.text = "HIGHSCORE: " + Score.GetHighScore();
    }
    Show();
  }
  private void Hide()
  {
    gameObject.SetActive(false);
  }
  private void Show()
  {
    gameObject.SetActive(true);
  }

}
