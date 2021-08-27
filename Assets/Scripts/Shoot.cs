using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //raycast on object
            //debug.log shows the hit object name
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Just hit: " + hit.transform.name);
            }
        }
    }
}
