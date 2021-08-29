using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1f;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;

    private CharacterController _controller;
    private Transform _player;
    private Health _playerHealth;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if(_controller == null)
        {
            Debug.LogError("Character Controller is NULL!");
        }
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
            Debug.LogError("Player or PlayerHealth is NULL");
    }

    private void Update()
    {
        if(_currentState == EnemyState.Chase)
            CalculateMovement();

        if(_currentState == EnemyState.Attack)
        {
            if(Time.time > _nextAttack)
            {
                _playerHealth.Damage(10);
                _nextAttack = Time.time + _attackDelay;
            }
        }
    }

    void CalculateMovement()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0;
        transform.localRotation = Quaternion.LookRotation(direction);
        Vector3 velocity = direction * _speed;
        if (!_controller.isGrounded)
        {
            velocity.y -= _gravity;
        }

        _controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _currentState = EnemyState.Attack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _currentState = EnemyState.Chase; 
    }
}
