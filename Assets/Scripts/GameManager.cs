using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level;
    int nextSceneIndex;
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

        this.level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void nextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
