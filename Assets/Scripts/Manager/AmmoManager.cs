using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private static AmmoManager _instance;
    public static AmmoManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("AmmoManager is NULL!");
            return _instance;
        }
    }

    [SerializeField]
    private int _currentAmmo;

    private bool _hasAmmo = true;

    private void Awake()
    {
        _instance = this;
        UIManager.Instance.UpdateAmmo(_currentAmmo);
    }

    //add ammo method
    //communicate with UIManager
    public void AddAmmo(int pickedAmmo)
    {
        _hasAmmo = true;
        _currentAmmo += pickedAmmo;
        UIManager.Instance.UpdateAmmo(_currentAmmo);
    }

    //decrease ammo int method
    //if ammo > 0
        //decrease 1 and return value to UIManager
    //else
        //return 0 value to UIManager
        //return to GunFirePistol
    public void DecreaseAmmo()
    {
        _currentAmmo--;
        if(_currentAmmo > 0)
        {
            UIManager.Instance.UpdateAmmo(_currentAmmo);
        }
        else
        {
            _currentAmmo = 0;
            UIManager.Instance.UpdateAmmo(0);
            _hasAmmo = false;
        }
    }

    public bool HasAmmo()
    {
        return _hasAmmo;
    }
}
