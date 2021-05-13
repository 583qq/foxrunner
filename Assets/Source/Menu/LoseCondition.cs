using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseCondition : MonoBehaviour
{
    private Player player;
    
    public void Start()
    {
        player = GameReferences.player;
    }

    public void InvokeForce()
    {
        Action();
    }

    public void InvokeWhen<T>(Func<T, bool> predicate, T arg)
    {
        if(predicate == null)
            return;

        if(predicate(arg))
            Action();
    }

    private void Action()
    {
        Debug.Log("Going to menu.");

        // Save Progress
        SaveGameSceneProgress();

        // Change scene
        SceneManager.LoadScene("Menu");

        Debug.Log("Progress saved. Menu loaded.");
    }

    private void SaveGameSceneProgress()
    {
        UpgradesMenu.currency += player.score.value;
        Debug.Log($"Adding {player.score.value} points.");
    }
}