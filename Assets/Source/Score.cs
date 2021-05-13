using System;
using UnityEngine;


public class Score : MonoBehaviour
{
    [SerializeField] ScoreViewModel _vm;

    private int _score;

    public int value => _score;

    public void Add(int value)
    {
        _score += value;
        _vm.OnScoreChanged(value);
    }

    public void Reset()
    {
        _vm.OnScoreChanged(-value);
        _score = 0;
    }

    public void Subscribe(Action<int> del)
    {
        _vm.scoreChangedNotify += del;
    }

    public void Unsubscribe(Action<int> del)
    {
        _vm.scoreChangedNotify -= del;
    }
}