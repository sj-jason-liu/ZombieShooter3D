using System.Collections;
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
        if (gameObject.tag == "Player")
            UIManager.Instance.UpdateHealth(_currentHealth, _maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageAmount)
    {
        if (gameObject.tag == "Enemy")
            gameObject.GetComponent<EnemyAI>().StartChasing();
        _currentHealth -= damageAmount;
        if (gameObject.tag == "Player")
            UIManager.Instance.UpdateHealth(_currentHealth, _maxHealth);
        //Debug.Log("The health of " + gameObject.name + " is " + _currentHealth);
        if (_currentHealth < _minHealth)
        {
            if (gameObject.tag == "Enemy")
                gameObject.GetComponent<EnemyAI>().EnemyDeath();
        }
    }
}
