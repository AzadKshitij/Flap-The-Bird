using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charactercustmization : MonoBehaviour
{
  Image imageComponent;
  public Sprite[] players;
  private int playerIndex = 0;

  // Start is called before the first frame update
  void Start()
  {
    imageComponent = GetComponent<Image>();
    imageComponent.sprite = players[0];
  }

  public void Next()
  {
    if (playerIndex < players.Length - 1)
    {
      playerIndex++;
      imageComponent.sprite = players[playerIndex];
    }
  }

  public void Previous()
  {
    if (playerIndex > 0)
    {
      playerIndex = playerIndex - 1;
      imageComponent.sprite = players[playerIndex];
    }
  }
}

