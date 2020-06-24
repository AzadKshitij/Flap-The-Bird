using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdController : MonoBehaviour
{
  private static AdController instance;
  private string store_id = "3592091";
  // private string rewarded_video_ad = "rewardedVideo";
  private string banner_ad = "banner";

  IEnumerator Start()
  {
    Advertisement.Initialize(store_id);

    while (!Advertisement.IsReady())
      yield return null;

    Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    Advertisement.Banner.Show(banner_ad);
  }
}