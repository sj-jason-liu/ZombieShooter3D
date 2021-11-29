using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour, IPickable
{
    [SerializeField]
    private GameObject _fallingZombieEvent;

    void Update()
    {
        transform.Rotate(0, 0.3f, 0 * Time.deltaTime, Space.World);
    }

    public void PickUp()
    {
        GameManager.Instance.HasLabKey = true;
        _fallingZombieEvent.SetActive(true);
        Destroy(gameObject);
    }
}
