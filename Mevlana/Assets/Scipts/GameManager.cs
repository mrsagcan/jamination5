using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<Transform> playerDeployLocs = new List<Transform>();

    // Update is called once per frame
    void Awake()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = Instantiate(players[i], playerDeployLocs[i].position, playerDeployLocs[i].rotation);
            player.gameObject.GetComponent<PlayerMovementController>().playerId = i;
            Destroy(playerDeployLocs[i].gameObject);
        }
    }
}
