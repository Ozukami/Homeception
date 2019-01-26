using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;
  private int _nextSceneIndex;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }
  }

  // Update is called once per frame
  void Update()
  {
    _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
  }

  public void nextScene()
  {
    SceneManager.LoadScene(_nextSceneIndex);
  }
}
