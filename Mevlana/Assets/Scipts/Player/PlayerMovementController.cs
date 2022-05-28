using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("CombatAttributes")]
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private GameObject bullet;

    [Header("MovementAttributes")]
    [SerializeField] private float angularSpeed = 10f;
    [SerializeField] private float forwardSpeed = 100f;

    public int playerId;

    private Rigidbody playerRb;
    private Vector3 playerCurrentAngularVeloctiy;
    private bool isTurning;
    private bool isTurningRight;
    private KeyCode actionKey;

    // Start is called before the first frame update
    void Start()
    {
        if (playerId == 0)
        {
            actionKey = KeyCode.Space;
        }
        if (playerId == 1)
        {
            actionKey = KeyCode.Mouse0;
        }
        playerRb = GetComponent<Rigidbody>();
        playerCurrentAngularVeloctiy = Vector3.zero;
        isTurning = true;
        isTurningRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            isTurning = false;
            MoveForward();
        }
        if (Input.GetKeyUp(actionKey))
        {
            isTurning = true;
            playerRb.velocity = Vector3.zero;
            FireBasic();
            isTurningRight = !isTurningRight;
        }
    }

    private void FixedUpdate()
    {
        if (isTurning)
        {
            RotatePlayer(isTurningRight);
        }
        else
        {
            playerRb.angularVelocity = Vector3.zero;
        }
    }

    void RotatePlayer(bool isTurningRight)
    {
        if (isTurningRight)
        {
            playerRb.angularVelocity = new Vector3(0f, angularSpeed, 0f);
        }
        else
        {
            playerRb.angularVelocity = new Vector3(0f, -angularSpeed, 0f);
        }
    }

    void MoveForward()
    {
        Vector3 forwardVector = transform.forward;
        playerRb.AddForce(forwardVector * forwardSpeed, ForceMode.Acceleration);
    }

    void FireBasic()
    {
        Debug.Log("FireBasic");
        Instantiate(bullet, transform);
    }

    private void ResetTransform()
    {
        transform.rotation = Quaternion.identity;
    }
}
