using System;
using UnityEngine;
using UnityEngine.UI;


public class HealthViewModel : MonoBehaviour
{
    [SerializeField] private HealthView _view;

    public event Action<int> healthChangedNotify;

    public void OnHealthChanged(int delta)
    {
        if(delta == 0)
            return;

        bool isPositive = (delta > 0) ? true : false;

        if(isPositive)
            _view.AddViewIcon(delta);
        else
            _view.RemoveViewIcon(-delta);


        healthChangedNotify?.Invoke(delta);
    }
}