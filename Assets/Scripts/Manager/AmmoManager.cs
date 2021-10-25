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
    private int _currentAmmo, _maxMagazine;
    private int _magAmmo;

    private bool _canShoot = true;
    private bool _canReload = true;

    private void Awake()
    {
        _instance = this;
        _magAmmo = _maxMagazine;
        UIManager.Instance.UpdateAmmo(_magAmmo, _currentAmmo);
    }

    private void Update()
    {
        if(_currentAmmo == 0)
        {
            _canReload = false;
        }
    }

    public void AddAmmo(int pickedAmmo)
    {
        _canReload = true;
        _canShoot = true;
        _currentAmmo += pickedAmmo;
        UIManager.Instance.UpdateAmmo(_magAmmo, _currentAmmo);
    }

    public void DecreaseAmmo()
    {
        _magAmmo--;
        if(_magAmmo <= 0)
        {
            _magAmmo = 0;
            _canShoot = false;
        }
        UIManager.Instance.UpdateAmmo(_magAmmo, _currentAmmo);
    }

    public void ReloadMagazine()
    {
        if((_magAmmo + _currentAmmo) > _maxMagazine)
        {
            _currentAmmo -= _maxMagazine - _magAmmo;
            _magAmmo = _maxMagazine;
        }
        else
        {
            _magAmmo += _currentAmmo;
            _currentAmmo = 0;
        }
        _canShoot = true;
        UIManager.Instance.UpdateAmmo(_magAmmo, _currentAmmo);
    }

    public bool HasAmmo()
    {
        return _canShoot;
    }

    public bool CanReload()
    {
        return _canReload;
    }
}
