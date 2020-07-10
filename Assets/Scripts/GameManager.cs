using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int counterTime;

    [HideInInspector]
    public bool isGameRunning;
    [HideInInspector]
    public int selectedColor;
    [HideInInspector]
    public int currentColor;

    void Awake()
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

    public void TapToPlayButtonClick()
    {
        StartGame();
    }

    void StartGame()
    {
        isGameRunning = true;
        StartCoroutine(PlayerManager.instance.Movement());
        StartCoroutine(ObjectPoolManager.instance.ObjectSpawner());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
