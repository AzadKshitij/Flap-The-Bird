using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameAssets : MonoBehaviour
{

  private static gameAssets instace;

  public static gameAssets GetInstance()
  {
    return instace;
  }
  private void Awake()
  {
    instace = this;
  }
  public Sprite pipeHeadSprite;
  public Transform pfPipeHead;
  public Transform pfPipeBody;
  public Transform pfGround;
  public Transform pfCloud_1;
  public Transform pfCloud_2;
  public Transform pfCloud_3;


  public SoundAudioClip[] soundAudioClipArray;

  [Serializable]
  public class SoundAudioClip
  {
    public SoundManager.Sound sound;
    public AudioClip audioClip;
  }


}
