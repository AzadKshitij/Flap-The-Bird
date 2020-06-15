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
      Debug.Log(playerIndex + "  Player Index Next");
      imageComponent.sprite = players[playerIndex];
    }
  }

  public void Previous()
  {
    if (playerIndex > 0)
    {
      playerIndex = playerIndex - 1;
      Debug.Log(playerIndex + "  Player Index Previous");
      imageComponent.sprite = players[playerIndex];
    }
  }
}


// Image m_Image;
///     //Set this in the Inspector
///     public Sprite m_Sprite;
///
///     void Start()
///     {
///         //Fetch the Image from the GameObject
///         m_Image = GetComponent<Image>();
///     }
///
///     void Update()
///     {
///         //Press space to change the Sprite of the Image
///         if (Input.GetKey(KeyCode.Space))
///         {
///             m_Image.sprite = m_Sprite;
///         }