using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllbirdsGameScene : MonoBehaviour
{
  public GameObject[] allBirds;
  // Start is called before the first frame update

  private void Awake()
  {
    allBirds[0].SetActive(false);
    allBirds[1].SetActive(false);
    allBirds[2].SetActive(false);
    allBirds[3].SetActive(false);
    allBirds[4].SetActive(false);
    allBirds[5].SetActive(false);
  }
  void Start()
  {
    allBirds[PlayerPrefs.GetInt("PlayerPrefab")].SetActive(true);
  }
}
