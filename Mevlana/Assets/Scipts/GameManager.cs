using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<Transform> playerDeployLocs = new List<Transform>();
    public List<GameObject> flagPlatforms = new List<GameObject>();
    public List<Transform> flagDeployLocs = new List<Transform>();
           
    // Update is called once per frame
    void Awake()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = Instantiate(players[i], playerDeployLocs[i].position, playerDeployLocs[i].rotation);
            player.gameObject.GetComponent<PlayerMovementController>().playerId = i;
            Destroy(playerDeployLocs[i].gameObject);
            GameObject flagPlatform = Instantiate(flagPlatforms[i], flagDeployLocs[i].position, flagDeployLocs[i].rotation);
            flagPlatform.gameObject.transform.Find("Flag").GetComponent<Flag>().flagId = i;
            Destroy(flagDeployLocs[i].gameObject);
        }
    }
}
