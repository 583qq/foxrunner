using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private Text currencyField;
    public static int currency;
    public static bool init;

    public void Start()
    {        
        if(!init)
        {
            init = true;
            LoadSavedParams();
        }
    }

    public void Awake()
    {
        UpdateCurrencyText();
    }

    //  Extract Loading & Saving

    public void LoadSavedParams()
    {
        // FROM FILE?
    }

    public void SaveParams()
    {
        // TO FILE?
    }

    // ...

    public void UpdateCurrencyText()
    {
        currencyField.text = currency.ToString();
    }
}