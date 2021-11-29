using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour, IUnlockable
{
    [SerializeField]
    private GameObject _door, _openPos, _powerLight;

    [SerializeField]
    private float _doorSpeed = 0.2f;

    private bool _hasOpen, _hasPower;

    void Update()
    {
        if (_hasOpen)
        {
            _door.transform.position = Vector3.MoveTowards(_door.transform.position, _openPos.transform.position, _doorSpeed * Time.deltaTime);
        }

        if (_powerLight == null)
            return;
        else if (_hasPower)
        {
            _powerLight.SetActive(true);
        }
    }

    public void CheckDoorAvailable()
    {
        //showing press E to press the button
        //disable after 3 sec
        Debug.Log("Press E to press the button.");
    }

    public void OpenDoor()
    {
        if(GameManager.Instance.HasBattery)
        {
            _door.GetComponent<BoxCollider>().enabled = false;
            _hasOpen = true;
            _hasPower = true;
        }
        else
        {
            //showing battery needed text
            Debug.Log("Looks like I need the battery.");
        }

    }
}
