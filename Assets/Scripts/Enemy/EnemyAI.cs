using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1f;
    [SerializeField]
    private float _detectRange = 5f;

    private bool _isDead;

    [SerializeField]
    private EnemyState _currentState = EnemyState.Idle;

    [Space][Header("Set zombie as dead body")][SerializeField]
    private bool _deadBody;

    private CharacterController _controller;
    private Transform _player;
    private Health _playerHealth;
    private EnemyAnimation _animator;

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
        _animator = GetComponentInChildren<EnemyAnimation>();
        if (_animator == null)
            Debug.LogError("Enemy Animator is NULL!");

        if (_deadBody == true)
            EnemyDeath();
    }

    //if Player is out of range or Enemy has not been attacked
    //state is idle
    private void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Idle:
                CalculateGravity();
                break;
            case EnemyState.Chase:
                CalculateMovement();
                _animator.ChasingTrigger();
                break;
            case EnemyState.Attack:
                //if (Time.time > _nextAttack)
                //{
                //    if (_player != null)
                //        _playerHealth.Damage(10);

                //    _nextAttack = Time.time + _attackDelay;
                //}
                break;
            case EnemyState.Die:
                _isDead = true;
                break;
        }

        if (Vector3.Distance(transform.position, _player.position) < _detectRange && !_isDead)
        {
            _currentState = EnemyState.Chase;
            _animator.ChasingTrigger();
        }
    }

    void CalculateGravity()
    {
        if(!_controller.isGrounded)
            _controller.Move(transform.up * -_gravity * Time.deltaTime);
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
        _animator.ChasingTrigger();
    }

    public void Attacking()
    {
        if (_player != null)
            _playerHealth.Damage(10);
    }

    public void EnemyDeath()
    {
        _currentState = EnemyState.Die;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        _animator.Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isDead)
        {
            _currentState = EnemyState.Attack;
            _animator.StartAttacking(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !_isDead)
        {
            _currentState = EnemyState.Chase;
            _animator.StartAttacking(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.red;
    }
}
