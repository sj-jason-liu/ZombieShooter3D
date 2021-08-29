﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth, _minHealth;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        Debug.Log("The health of " + gameObject.name + " is " + _currentHealth);
        if (_currentHealth < _minHealth)
            Destroy(gameObject);
    }
}