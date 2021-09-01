﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed = 3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = _target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
    }
}
