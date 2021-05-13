using System;
using UnityEngine;


public class ScoreViewModel : MonoBehaviour
{
    [SerializeField] private ScoreView _view;

    public event Action<int> scoreChangedNotify;

    public void OnScoreChanged(int delta)
    {
        if(delta == 0)
            return;
        
        _view.AddDeltaScore(delta);

        scoreChangedNotify?.Invoke(delta);
    }
}