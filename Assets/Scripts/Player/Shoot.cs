using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodPrefab;

    // Update is called once per frame
    void Update()
    {
        ExecuteShoot();
    }

    void ExecuteShoot()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 0, QueryTriggerInteraction.Ignore))
            {
                Health health = hit.transform.GetComponent<Health>();
                if (health != null)
                {
                    GameObject instBlood = Instantiate(_bloodPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    instBlood.transform.SetParent(hit.transform);
                    Destroy(instBlood, 0.5f);
                    health.Damage(5);
                }
            }
        }*/
    }
}
