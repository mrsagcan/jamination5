using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [Header("CombatAttributes")]
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private GameObject bullet;

    [Header("MovementAttributes")]
    [SerializeField] private float angularSpeed = 10f;
    [SerializeField] private float forwardSpeed = 100f;
    
    //[SerializeField] private Image speedImg;
    //[SerializeField] private Image smallImg;
    private bool speedPowerActivated, smallPowerActivated;

    public int playerId;

    private Rigidbody playerRb;
    private Vector3 playerCurrentAngularVeloctiy;
    private bool isTurning;
    private bool isTurningRight;
    private KeyCode actionKey;
    private float holdTime;

    [Header("PlayerAttributes")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float spawnSecondsAfter = 2f;
    private GameManager gameManager;
    private Vector3 spawnedPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnedPosition = transform.position;
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
            FireBasic();
            isTurningRight = !isTurningRight;
        }
        if (isTurning)
        {
            ResetTransform();
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
        if (Time.time > holdTime + cooldownTime)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            holdTime = Time.time;
        }
    }

    private void ResetTransform()
    {
        playerRb.velocity = Vector3.zero;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("speed"))
        {
            powerUp(0);
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("sizeSmaller"))
        {
            powerUp(1);
            Destroy(other.gameObject);
        }
    }
    

    private void powerUp(int pwr)
    {
        switch (pwr)
        {
            case 0:
                speedPowerActivated = true;
                forwardSpeed += forwardSpeed / 2;
                playerRb.AddForce(transform.forward * forwardSpeed/2, ForceMode.Acceleration);
                Invoke("finishPowerUp", 5);
                break;
            case 1:
                gameObject.transform.localScale /= 2;
                smallPowerActivated = true;
                Invoke("finishPowerUp", 5);
                break;
        }
    }

    //Will be deleted after UI added
    private void finishPowerUp()
    {
        if (speedPowerActivated)
        {
            forwardSpeed -= forwardSpeed / 2;
            playerRb.AddForce(-transform.forward * forwardSpeed/2, ForceMode.Acceleration);
            speedPowerActivated = false;
        }

        if (smallPowerActivated)
        {
            gameObject.transform.localScale *= 2;
            smallPowerActivated = false;

        }
        
    }

    public void OnHitDie()
    {
        PlayerFlagController flagControllerRef = gameObject.GetComponent<PlayerFlagController>();
        flagControllerRef.isCarrying = false;
        StartCoroutine(gameManager.SpawnAgain(gameObject, spawnSecondsAfter, spawnedPosition));
    }
}
