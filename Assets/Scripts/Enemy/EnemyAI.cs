using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 1f;

    //reference of character controller
    private CharacterController _controller;

    private Player _player;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if(_controller == null)
        {
            Debug.LogError("Character Controller is NULL!");
        }
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL!");
        }
    }

    private void Update()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        direction.y = 0;
        transform.localRotation = Quaternion.LookRotation(direction);
        Vector3 velocity = direction * _speed;
        if (!_controller.isGrounded)
        {
            velocity.y -= _gravity;
        }

        _controller.Move(velocity * Time.deltaTime);
    }
}
