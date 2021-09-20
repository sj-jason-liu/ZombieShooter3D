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
    private float _detectRange = 5f;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Idle;

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

    //if Player is out of range or Enemy has not been attacked
    //state is idle
    private void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Attack:
                if (Time.time > _nextAttack)
                {
                    if (_player != null)
                        _playerHealth.Damage(10);

                    _nextAttack = Time.time + _attackDelay;
                }
                break;
        }

        if (Vector3.Distance(transform.position, _player.position) < _detectRange)
            _currentState = EnemyState.Chase;
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

    public void StartChasing()
    {
        _currentState = EnemyState.Chase;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
    }
}
