using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    private int _ammoAmount = 15;
    
    void Update()
    {
        transform.Rotate(0, 0.3f, 0 * Time.deltaTime, Space.World);
    }

    public int GetAmmo()
    {
        return _ammoAmount;
    }
}
