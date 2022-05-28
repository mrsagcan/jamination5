using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagController : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject flag;

    private void Update()
    {
        if(isCarrying)
        {
            flag.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Flag"))
        {
            flag = other.gameObject;
            isCarrying = true;
        }
    }
}
