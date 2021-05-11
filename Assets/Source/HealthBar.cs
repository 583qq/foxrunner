using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthIcon;
    private int maxHealth;

    public int healthIconsCount => healthIcons.Count;

    [SerializeField] private int padding = 10;

    private List<GameObject> healthIcons;

    public void Start()
    {
       healthIcons = new List<GameObject>();

       int currentHealth = GameReferences.player.health;
       int maxHealth = GameReferences.player.maxHealth;
        
       for(int i = 0; i < currentHealth; i++)
       {
        Vector3 offset = new Vector3(i * padding, 0, 0);

        GameObject healthIcon = GameObject.Instantiate(_healthIcon, transform.position, 
                                                        Quaternion.identity, transform);

        healthIcons.Add(healthIcon);

        healthIcon.transform.localPosition += offset;
       }
    }

    public void RemoveIcon(int value)
    {
        Debug.Log($"Removing {value} Health Icons");

        if(healthIconsCount < 1)
        {
            Debug.Log("No Icons to remove.");
            return;
        }

        if(value >= healthIconsCount)
        {
            for(int i = 0; i < healthIconsCount; i++)
            {
                Destroy(healthIcons[i]);
            }
            healthIcons = new List<GameObject>();

            return;
        }

        for(int i = healthIconsCount - 1, k = 0; k < value; i--, k++)
        {
            GameObject icon = healthIcons[i];
            healthIcons.RemoveAt(i);
            Destroy(icon);
        }
    }

    public void AddIcon(int value)
    {
        if(healthIconsCount == maxHealth)
            return;

        Debug.Log($"Adding {value} health icons.");

        for(int i = 0; i < value; i++)
        {
            int k = healthIconsCount + i;

            Vector3 offset = new Vector3(k * padding, 0, 0);
    

            GameObject healthIcon = GameObject.Instantiate(_healthIcon, transform.position, 
                                                            Quaternion.identity, transform);

            healthIcons.Add(healthIcon);

            healthIcon.transform.localPosition += offset;
        } 
    }



}