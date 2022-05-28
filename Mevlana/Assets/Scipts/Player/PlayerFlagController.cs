using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagController : MonoBehaviour
{
    public bool isCarrying = false;
    public Vector3 otherTeamFlagPlatformPostition;

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
            Debug.Log("Flag collided");
            flag = other.gameObject;
            Flag flagRef = flag.GetComponent<Flag>();
            PlayerMovementController playerRef = transform.GetComponent<PlayerMovementController>();
            if(flagRef.flagId != playerRef.playerId)
            {
                isCarrying = true;
            }
            else if(flagRef.flagId == playerRef.playerId)
            {
                flag.transform.position = flagRef.flagInitPosition;
            }
        }

        if (other.gameObject.CompareTag("FlagPlatform") && isCarrying)
        {
            int platformId = other.gameObject.GetComponent<FlagPlatformController>().platformId;
            int playerId = gameObject.GetComponent<PlayerMovementController>().playerId;
            if (playerId == platformId)
            {
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.ScoreOneForPlayer(gameObject.GetComponent<PlayerMovementController>().playerId);
            }
        }
    }


}
