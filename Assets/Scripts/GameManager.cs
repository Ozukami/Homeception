using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  private GameObject _endLanternOn;
  private GameObject _endLanternOff;
  private GameObject _endPoint;

  public GameObject EndPoint => _endPoint;

  private void Awake() {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);

    _endPoint = GameObject.Find("EndPoint");
    _endLanternOff = GameObject.Find("EndLanternOff");
    _endLanternOn = GameObject.Find("EndLanternOn");
    _endLanternOn.SetActive(false);
  }

  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.N)) NextScene();
  }

  public void NextScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 % SceneManager.sceneCount);
  }

  public void DropLantern() {
    if (_endLanternOff.activeSelf)
      _endLanternOff.SetActive(false);
    if (!_endLanternOn.activeSelf)
      _endLanternOn.SetActive(true);
  }
}
