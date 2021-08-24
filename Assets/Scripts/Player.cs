using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _character;

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpForce = 3f;
    [SerializeField]
    private float _rotateSpeed = 8f;
    private float _yVelocity;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<CharacterController>();
        if (_character == null)
        {
            Debug.LogError("Character Controller is NULL!");
        }
        _camera = Camera.main;
        if(_camera == null)
        {
            Debug.LogError("Camera is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //move mouse x to rotate Player.y == left and right
        //move mouse y to rotate camera.x
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _camera.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY;
        _camera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    void Movement()
    {
        float horiInput = Input.GetAxisRaw("Horizontal");
        float vertiInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horiInput, 0, vertiInput);
        Vector3 velocity = direction * _speed;

        if (_character.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpForce;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _character.Move(velocity * Time.deltaTime);
    }
}
