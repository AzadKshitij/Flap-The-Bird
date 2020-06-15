using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public static class SoundManager
{

  public enum Sound
  {
    BirdJump,
    Score,
    Lose,
    ButtonOver,
    ButtonClick,
  }
  public static void PlaySound(Sound sound)
  {
    GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
    // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
    // audioSource.PlayOneShot(gameAssets.GetInstance().birdJump);
    audioSource.PlayOneShot(GetAudioClip(sound));
  }

  private static AudioClip GetAudioClip(Sound sound)
  {
    foreach (gameAssets.SoundAudioClip soundAudioClip in gameAssets.GetInstance().soundAudioClipArray)
    {
      if (soundAudioClip.sound == sound)
      {
        return soundAudioClip.audioClip;
      }
    }
    Debug.LogError("Sound " + sound + " not found!");
    return null;
  }

  public static void AddButtonSounds(this Button_UI buttonUI)
  {
    // buttonUI.MouseOverOnceFunc += () => PlaySound(Sound.ButtonOver);
    buttonUI.ClickFunc += () => PlaySound(Sound.ButtonClick);
  }

}
