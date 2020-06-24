using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldHandeler : MonoBehaviour
{
  public Text goldCoinText;
  private static GoldHandeler instance;


  public static GoldHandeler GetInstance()
  {
    return instance;
  }
  private void Awake()
  {
    instance = this;
    setCoinText();
  }

  public void setCoins(int scoreValue)
  {
    int coins = scoreValue / 5;
    PlayerPrefs.SetInt("goldCoins", PlayerPrefs.GetInt("goldCoins") + coins);
  }

  public void setCoinText()
  {
    goldCoinText.text = PlayerPrefs.GetInt("goldCoins").ToString();
  }

}
