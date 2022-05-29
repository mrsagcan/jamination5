using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    
    [Header("CombatAttributes")]
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private GameObject bullet;

    [Header("MovementAttributes")]
    [SerializeField] private float angularSpeed = 10f;
    [SerializeField] private float forwardSpeed = 100f;
    private float firstSpeed;
    
    
    private bool speedPowerActivated, smallPowerActivated, coolDownActivated;
    private float currentX;
    [SerializeField] private int powerUpReturnTime;
    private Vector3 destination = new Vector3(999,999,999);

    public GameObject bulletTrailPS;
    public int playerId;
    public int skor;

    private Rigidbody playerRb;
    private Vector3 playerCurrentAngularVeloctiy;
    private bool isTurning;
    private bool isTurningRight;
    private KeyCode actionKey;
    private float holdTime;
    [SerializeField] public bool justAttacked;

    [Header("PlayerAttributes")]
    [SerializeField] public float cooldownTime;
    [SerializeField] private float spawnSecondsAfter = 2f;
    private GameManager gameManager;
    private Vector3 spawnedPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentX = transform.localScale.x;
        firstSpeed = forwardSpeed;
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
        checkScore();
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
        if (Time.time > holdTime + cooldownTime)
        {
            GameObject bulletToBeFollowed = Instantiate(bullet, gunPoint.transform.position, gunPoint.transform.rotation);
            GameObject bulletTrailRef = Instantiate(bulletTrailPS, gunPoint.transform.position, gunPoint.transform.rotation);
            bulletTrailRef.GetComponent<BulletTrailParticleSystem>().bulletToBeFollowed = bulletToBeFollowed;
            holdTime = Time.time;
            justAttacked = true;
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
            StartCoroutine(powerUpTurner(powerUpReturnTime, other.gameObject, destination, other.gameObject.transform.position));
        } else if (other.gameObject.CompareTag("sizeSmaller"))
        {
            powerUp(1);
            StartCoroutine(powerUpTurner(powerUpReturnTime, other.gameObject, destination, other.gameObject.transform.position));        }
        else if (other.gameObject.CompareTag("coolDown"))
        {
            powerUp(2);
            StartCoroutine(powerUpTurner(powerUpReturnTime, other.gameObject, destination, other.gameObject.transform.position));        }
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
                if(gameObject.transform.localScale.x == currentX)
                    gameObject.transform.localScale /= 2;
                smallPowerActivated = true;
                Invoke("finishPowerUp", 5);
                break;
            case 2:
                cooldownTime /= 2;
                coolDownActivated = true;
                Invoke("finishPowerUp",5);
                break;
        }
    }

    //Will be deleted after UI added
    private void finishPowerUp()
    {
        if (speedPowerActivated)
        {
            forwardSpeed -= firstSpeed / 2;
            playerRb.AddForce(-transform.forward * forwardSpeed/2, ForceMode.Acceleration);
            speedPowerActivated = false;
        }

        if (smallPowerActivated)
        {
            gameObject.transform.localScale *= 2;
            smallPowerActivated = false;

        }
        
        if (coolDownActivated)
        {
            cooldownTime *= 2;
            coolDownActivated = false;

        }
        
    }

    public void OnHitDie()
    {
        PlayerFlagController flagControllerRef = gameObject.GetComponent<PlayerFlagController>();
        flagControllerRef.isCarrying = false;
        StartCoroutine(gameManager.SpawnAgain(gameObject, spawnSecondsAfter, spawnedPosition));
    }

    IEnumerator powerUpTurner(int time, GameObject powerUp, Vector3 destination, Vector3 currentPosition)
    {
        powerUp.transform.position = destination;
        yield return new WaitForSeconds(time);
        powerUp.transform.position = currentPosition;

    }

    private void checkScore()
    {
        if (playerId == 0)
        {
            skor = gameManager.playerScores[0];
            if (skor == 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            skor = gameManager.playerScores[1];
            if (skor == 4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
