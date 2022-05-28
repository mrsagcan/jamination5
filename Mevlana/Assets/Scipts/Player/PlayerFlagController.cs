using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagController : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject flag;
    private Vector3 flagCarryPoint;

    private void Update()
    {
        if(isCarrying)
        {
            flagCarryPoint = gameObject.transform.Find("FlagCarryPoint").GetComponent<Transform>().position;
            flag.transform.position = flagCarryPoint;
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
