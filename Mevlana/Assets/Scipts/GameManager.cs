using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<Transform> playerDeployLocs = new List<Transform>();
    public List<GameObject> flagPlatforms = new List<GameObject>();
    public List<Transform> flagDeployLocs = new List<Transform>();
    public List<int> playerScores = new List<int>();
           
    // Update is called once per frame
    void Awake()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = Instantiate(players[i], playerDeployLocs[i].position, playerDeployLocs[i].rotation);
            player.gameObject.GetComponent<PlayerMovementController>().playerId = i;
            Destroy(playerDeployLocs[i].gameObject);
            GameObject flagPlatform = Instantiate(flagPlatforms[i], flagDeployLocs[i].position, flagDeployLocs[i].rotation);
            flagPlatform.GetComponent<FlagPlatformController>().platformId = i;
            flagPlatform.gameObject.transform.Find("Flag").GetComponent<Flag>().flagId = i;
            playerScores.Add(0);
        }
    }

    public List<int> ScoreOneForPlayer(int playerId)
    {
        playerScores[playerId]++;
        return playerScores;
    }

    public IEnumerator SpawnAgain(GameObject _gameObject, float time, Vector3 spawnPosition)
    {

        _gameObject.transform.position = new Vector3(999, 999, 999);
        yield return new WaitForSeconds(time);
        Debug.Log("Spawned Again");
        _gameObject.transform.position = spawnPosition;
    }

    public void ReSpawnAllFlags()
    {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");
        for (int i = 0; i < players.Count; i++)
        {
            Destroy(flags[i].gameObject);
            GameObject flagPlatform = Instantiate(flagPlatforms[i], flagDeployLocs[i].position, flagDeployLocs[i].rotation);
            flagPlatform.GetComponent<FlagPlatformController>().platformId = i;
            flagPlatform.gameObject.transform.Find("Flag").GetComponent<Flag>().flagId = i;
        }
     }
}
