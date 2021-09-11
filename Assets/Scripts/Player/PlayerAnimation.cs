using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private float _speed;
    private bool _isWalking = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _speed);
        _animator.SetBool("IsWalking", _isWalking);
    }

    public void MovingSpeed(float speed)
    {
        _speed = speed;
    }

    public void IsWalking(bool walkingState)
    {
        _isWalking = walkingState;
    }
}
