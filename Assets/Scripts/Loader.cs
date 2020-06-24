using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

  public enum Scene
  {
    Game,
    LoadingScene,
    MainMenuScene,
    CreditScene,
    CustmizeScene
  }

  private static Scene targetScene;

  public static void Load(Scene scene)
  {
    SceneManager.LoadScene(Scene.LoadingScene.ToString());
    targetScene = scene;
  }

  public static void LoadTargetScene()
  {
    SceneManager.LoadScene(targetScene.ToString());
  }

}
