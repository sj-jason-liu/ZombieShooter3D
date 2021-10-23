using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _character;
    private PlayerAnimation _anim;

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpForce = 3f;
    [SerializeField] [Range(0.5f, 2f)]
    private float _mouseSensitivity = 1f;
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
        if (_camera == null)
        {
            Debug.LogError("Camera is NULL!");
        }
        Cursor.lockState = CursorLockMode.Locked;
        _anim = GetComponent<PlayerAnimation>();
        if (_anim == null)
        {
            Debug.LogError("Player Animation is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatingMovement();
        CameraMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    void CalculatingMovement()
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
        velocity = transform.TransformDirection(velocity);
        _character.Move(velocity * Time.deltaTime);
        _anim.MovingSpeed(vertiInput);
        _anim.StrafeSpeed(horiInput);

        if (horiInput != 0 || vertiInput != 0)
        {
            _anim.IsWalking(true);
        }
        else
        {
            _anim.IsWalking(false);
        }
        
    }

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _mouseSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _camera.transform.localEulerAngles;
        currentCameraRotation.x += mouseY * -1 * _mouseSensitivity;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0f, 8f);
        _camera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ammo")
        {
            int getAmmo = other.GetComponent<Ammo>().GetAmmo();
            AmmoManager.Instance.AddAmmo(getAmmo);
            Destroy(other.gameObject);
        }
    }
}
