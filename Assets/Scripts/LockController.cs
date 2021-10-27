using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField]
    private GameObject _button, _door;

    [SerializeField]
    private float _doorSpeed = 0.2f;

    private bool _isPressed;
    
    void Update()
    {
        if(_isPressed)
        {
            if(_door.transform.position.z < -16.5f)
            {
                _door.transform.Translate(Vector3.left * _doorSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _door.GetComponent<BoxCollider>().enabled = false;
                _isPressed = true;
            }
        }
    }
}
