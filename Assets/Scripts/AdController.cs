using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdController : MonoBehaviour, IUnityAdsListener
{
  private string store_id = "3592091";
  private string video_ad = "video";
  private string rewarded_video_ad = "rewardedVideo";
  private string banner_ad = "banner";

  IEnumerator Start()
  {
    Advertisement.AddListener(this);
    Advertisement.Initialize(store_id);

    while (!Advertisement.IsReady())
      yield return null;

    Advertisement.Banner.SetPosition(BannerPosition.
    BOTTOM_CENTER);
    Advertisement.Banner.Show(banner_ad);
  }

  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  {
    if (showResult == ShowResult.Finished)
    {
      Debug.Log("Kshitij");
      // Reward the user for watching the ad to completion.
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

  public void OnUnityAdsDidStart(string placementId)
  {
    Debug.Log("The ad started playing.");
  }

  public void OnUnityAdsReady(string placementId)
  {
    // Advertisement.Show(placementId);
    Debug.Log("OnUnityAdsReady");
  }

  public void OnUnityAdsDidError(string errorMessage)
  {
    Debug.LogWarning(errorMessage);
  }






  // // Implement IUnityAdsListener interface methods:
  // public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  // {
  //   // Define conditional logic for each ad completion status:
  //   if (showResult == ShowResult.Finished)
  //   {
  //     // Reward the user for watching the ad to completion.
  //   }
  //   else if (showResult == ShowResult.Skipped)
  //   {
  //     // Do not reward the user for skipping the ad.
  //   }
  //   else if (showResult == ShowResult.Failed)
  //   {
  //     Debug.LogWarning("The ad did not finish due to an error.");
  //   }
  // }

  // public void OnUnityAdsReady(string placementId)
  // {
  //   // If the ready Placement is rewarded, show the ad:
  //   if (placementId == rewarded_video_ad)
  //   {
  //     Advertisement.Show(rewarded_video_ad);
  //   }
  // }

  // public void OnUnityAdsDidError(string message)
  // {
  //   // Log the error.
  //   Debug.Log("Error");
  // }

  // //   public void OnUnityAdsDidStart(string placementId)
  // //   {
  // //     // Optional actions to take when the end-users triggers an ad.

  // //   }


  // // // Start is called before the first frame update
  // // void Start()
  // // {
  // //     Monetization.Initialize(store_id,true); // for production mode we will type false
  // // }

  // // // Update is called once per frame
  // // void Update()
  // // {
  // //     if (Input.GetKeyDown(KeyCode.E)){
  // //         if(Monetization.IsReady(video_ad)){
  // //             showAdPlacementContent ad = null;
  // //             ad = Monetization.GetPlacementContent(video_ad) as showAdPlacementContent;

  // //             if(ad != null){
  // //                 ad.Show();
  // //             }
  // //         }
  // //     }

  // // }
}















