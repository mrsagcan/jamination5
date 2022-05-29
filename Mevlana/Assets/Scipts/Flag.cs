using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public int flagId;
    public Vector3 flagInitPosition;

    private PlayerFlagController playerFlagController;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        flagInitPosition = transform.position;

    }
}
