using System;
using UnityEngine;
using UnityEngine.UI;


public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private int _score;

    private void SetScore(int value)
    {
        _score = value;
        _scoreText.text = _score.ToString();
    }

    public void AddDeltaScore(int delta)
    {
        SetScore(_score + delta);
    }
}