using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is NULL!");
            return _instance;
        }
    }

    [SerializeField]
    private Text _ammoText, _magText;
    [SerializeField]
    private Slider _healthBar;
    private int _ammoCount;
    private int _magCount;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateAmmo(int magCount, int ammoCount)
    {
        _ammoText.text = "/ " + ammoCount;
        _magText.text = "" + magCount;
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthValue = (float) currentHealth / maxHealth;
        _healthBar.value = healthValue;
    }
}
