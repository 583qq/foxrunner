using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject _healthIcon;
    [SerializeField] private Transform _healthBar;
    [SerializeField] private float padding;

    private List<GameObject> _icons;

    private int iconsCount => _icons.Count;

    public void Awake()
    {
        _icons = new List<GameObject>();
    }

    public void AddViewIcon(int value)
    {
        Debug.Log($"[Health View] Adding {value} health icons.");

        for(int i = 0; i < value; i++)
        {
            int k = iconsCount + i;

            Vector3 offset = new Vector3(k * padding, 0, 0);
    
            GameObject icon = GameObject.Instantiate(_healthIcon, transform.position, 
                                                      Quaternion.identity, transform);

            _icons.Add(icon);

            icon.transform.localPosition += offset;
        } 
    }

    public void RemoveViewIcon(int value)
    {
        Debug.Log($"[Health View] Removing {value} health icons.");

        for(int i = iconsCount - 1, k = 0; k < value; i--, k++)
        {
            GameObject icon = _icons[i];
            _icons.RemoveAt(i);
            Destroy(icon);
        }
    }
}
