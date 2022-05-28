using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public int flagId;
    public Vector3 flagInitPosition;

    private void Start()
    {
        flagInitPosition = transform.position;
    }
}
