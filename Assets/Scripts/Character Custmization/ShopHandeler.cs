using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopHandeler : MonoBehaviour
{
  public GameObject[] buybutton;
  public GameObject[] lockObject;
  public Text shopGoldCoinText;
  public int i = 0;
  private void Awake()
  {
    // Debug.Log(PlayerPrefs.GetInt("valueofi") + "Value of i");
    shopGoldCoinText.text = PlayerPrefs.GetInt("goldCoins").ToString();
    BoughtBirds();
  }
  void Start()
  {
    // PlayerPrefs.DeleteKey("valueofi");
    // // Debug.Log(PlayerPrefs.GetInt("valueofi") + "Value of i Start");
    // PlayerPrefs.DeleteKey("highscore0");
    // PlayerPrefs.DeleteKey("highscore1");
    // PlayerPrefs.DeleteKey("highscore2");
    // PlayerPrefs.DeleteKey("highscore3");
    // PlayerPrefs.DeleteKey("highscore4");
  }

  public void BoughtBirds()
  {
    buybutton[PlayerPrefs.GetInt("highscore0")].SetActive(false);
    for (int i = 0; i < 6; i++)
    {
      if (PlayerPrefs.GetInt("highscore" + i) != 0)
      {
        // lockObject[PlayerPrefs.GetInt("highscore" + i)].SetActive(false);
        buybutton[PlayerPrefs.GetInt("highscore" + i)].SetActive(false);
      }
    }
  }

  public void BuyButton(int number)
  {
    PlayerPrefs.SetInt("highscore" + number, number);
    PlayerPrefs.Save();
    buybutton[number].SetActive(false);
    i++;
    PlayerPrefs.SetInt("valueofi", i);
    PlayerPrefs.Save();
    // Debug.Log(PlayerPrefs.GetInt("valueofi"));
    // Debug.Log(PlayerPrefs.GetInt("highscore3") + " highscore3 ");
  }

  public void PlayButton(int prefabNumber)
  {
    PlayerPrefs.SetInt("PlayerPrefab", prefabNumber);
    PlayerPrefs.Save();
    SceneManager.LoadScene(2);
  }

  public void changeScene()
  {
    SceneManager.LoadScene(0);
  }
}
