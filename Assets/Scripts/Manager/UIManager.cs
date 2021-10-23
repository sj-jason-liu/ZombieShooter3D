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
    private Text _ammoText;
    private int _ammoCount;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateAmmo(int ammoCount)
    {
        _ammoText.text = "Ammo: " + ammoCount;
    }
}
