using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPickable
{

    
    void Update()
    {
        transform.Rotate(0, 0.3f, 0 * Time.deltaTime, Space.World);
    }

    public void PickUp()
    {
        GameManager.Instance.HasBattery = true;
        Destroy(gameObject);
    }
}
