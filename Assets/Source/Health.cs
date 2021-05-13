using System;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private HealthViewModel _vm;
    [SerializeField] private int _maximum;
    private int _current;

    public int maximum => _maximum;
    public int current => _current;

    [SerializeField] private LoseCondition _lose;

    public void Start()
    {
        _current = maximum;
        _vm.OnHealthChanged(maximum);
    }

    public void Subscribe(Action<int> del)
    {
        _vm.healthChangedNotify += del;
    }

    public void Unsubscribe(Action<int> del)
    {
        _vm.healthChangedNotify -= del;
    }

    public void TakeDamage(int value)
    {
        if(value < 0)
            return;


        if(value >= current)
        {
            _lose.InvokeForce();
            return;
        }

        _current -= value;

        _vm.OnHealthChanged(-value);
    }

    public void Restore(int value)
    {
        if(value < 0)
            return;

        if(current + value > maximum)
            value = maximum - current;

        _current += value;

        _vm.OnHealthChanged(value);
    }

}