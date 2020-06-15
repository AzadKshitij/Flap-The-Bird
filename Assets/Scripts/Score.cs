using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
  public static void Start()
  {
    // ResetHighscore();
    Bird.GetInstance().OnDied += Bird_OnDied;
  }

  public static void Bird_OnDied(object sender, System.EventArgs e)
  {
    TrySetNEwHighscore(Level.GetInstance().GetPipePassed());
  }

  public static int GetHighScore()
  {
    return PlayerPrefs.GetInt("highscore");
  }


  public static bool TrySetNEwHighscore(int score)
  {
    int currentHighscore = GetHighScore();
    if (score > currentHighscore)
    {
      // new highscore
      PlayerPrefs.SetInt("highscore", score);
      PlayerPrefs.Save();
      return true;
    }
    else
    {
      return false;
    }
  }
  public static void ResetHighscore()
  {
    PlayerPrefs.SetInt("highscore", 0);
    PlayerPrefs.Save();
  }
}
