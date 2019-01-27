using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  private static GameManager _instance;

  public GameManager Instance {
    get {
      if (_instance == null)
        _instance = this;
      else if (_instance != this)
        Destroy(gameObject);
      return _instance;
    }
  }

  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.N)) NextScene();
  }

  public void NextScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 % SceneManager.sceneCount);
  }
}
