using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Scenes: " + SceneManager.sceneCount);
    }

    public void LoadBaseGameScene()
    {
        Debug.Log("Loading Base...");
        SceneManager.LoadScene("Base", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
