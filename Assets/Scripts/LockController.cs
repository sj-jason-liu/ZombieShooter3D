using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour, IUnlockable
{
    [SerializeField]
    private GameObject _door, _openPos;

    [SerializeField]
    private float _doorSpeed = 0.2f;

    private bool _hasOpen;
    
    void Update()
    {
        if(_hasOpen)
        {
            _door.transform.position = Vector3.MoveTowards(_door.transform.position, _openPos.transform.position, _doorSpeed * Time.deltaTime);
        }
    }

    public void CheckDoorAvailable()
    {
        if(GameManager.Instance.HasLabKey)
        {
            _door.GetComponent<BoxCollider>().enabled = false;
            _hasOpen = true;
        }
        else
        {
            //showing UI key card needed text
            //disable after 3 sec
            Debug.Log("Keycard needed.");
        }
    }

    public void OpenDoor()
    {
        //do nothing
    }
}
