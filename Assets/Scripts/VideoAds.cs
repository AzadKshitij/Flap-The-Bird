using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class VideoAds : MonoBehaviour, IUnityAdsListener
{

  string gameId = "3592091";
  string myPlacementId = "rewardedVideo";
  bool testMode = true;
  private static VideoAds instance;


  public static VideoAds GetInstance()
  {
    return instance;
  }

  private void Awake()
  {
    instance = this;

  }

  // Initialize the Ads listener and service:
  void Start()
  {
    Advertisement.AddListener(this);
    Advertisement.Initialize(gameId, testMode);
  }

  public void DisplayVideoAd()
  {
    Advertisement.Show(myPlacementId);
  }

  // Implement IUnityAdsListener interface methods:
  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  {
    // Define conditional logic for each ad completion status:
    if (showResult == ShowResult.Finished)
    {
      // SceneManager.LoadScene(2);
      Debug.Log("Kshitij");
    }
    else if (showResult == ShowResult.Skipped)
    {
      // Do not reward the user for skipping the ad.
    }
    else if (showResult == ShowResult.Failed)
    {
      Debug.LogWarning("The ad did not finish due to an error.");
    }
  }

  public void OnUnityAdsReady(string placementId)
  {
    // If the ready Placement is rewarded, show the ad:
    if (placementId == myPlacementId)
    {
    }
  }

  public void OnUnityAdsDidError(string message)
  {
    // Log the error.
  }

  public void OnUnityAdsDidStart(string placementId)
  {
    // Optional actions to take when the end-users triggers an ad.
  }
}