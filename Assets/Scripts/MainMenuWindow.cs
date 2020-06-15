using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenuWindow : MonoBehaviour
{
  private void Awake()

  {
    transform.Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.Game); };
    transform.Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();

    transform.Find("creditBtn").GetComponent<Button_UI>().ClickFunc = () => { Loader.Load(Loader.Scene.CreditScene); };
    transform.Find("creditBtn").GetComponent<Button_UI>().AddButtonSounds();

    transform.Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => { Application.Quit(); };
    transform.Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();
  }
}
