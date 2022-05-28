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
        List<GameObject> flagPlaforms = gameManager.flagPlatforms;

        for (int i = 0; i < flagPlaforms.Count; i++)
        {
            if(flagPlaforms[i].transform.Find("Flag").GetComponent<Flag>().flagId != flagId)
            {
                Vector3 otherTeamFlagPosition = flagPlaforms[i].transform.Find("Flag").transform.position;
            }

            
        }

        flagInitPosition = transform.position;

    }
}
