using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Image spdImg, smallImg, coolImg, spdImg2, smallImg2, coolImg2,p1cooldwn, p2cooldwn;
    [SerializeField] private List<Image> puImages;
    private int powerUp;
    private int playerIdUI = -1;
    private GameObject[] players;
    float currentCooldownTime = 0;
    public bool firstMoment;

    private void Start()
    {
        
        if (gameObject.name == "speedPwr")
        {
            powerUp = 0;
        } else if (gameObject.name == "smallPwr")
        {
            powerUp = 1;
        }
        else
        {
            powerUp = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name);
            return;
        }
        
        playerIdUI = other.gameObject.GetComponent<PlayerMovementController>().playerId;
        if (playerIdUI ==0)
        {
            switch (powerUp)
            {
                case 0:
                    spdImg.gameObject.SetActive(true);
                    break;
                case 1:
                    smallImg.gameObject.SetActive(true);
                    break;
                case 2:
                    coolImg.gameObject.SetActive(true);
                    break;
            }
        } else if (playerIdUI == 1)
        {
            switch (powerUp)
            {
                case 0:
                    spdImg2.gameObject.SetActive(true);
                    break;
                case 1:
                    smallImg2.gameObject.SetActive(true);
                    break;
                case 2:
                    coolImg2.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void Update()
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerMovementController>().justAttacked)
            {
                if (player.GetComponent<PlayerMovementController>().playerId == 0)
                {
                    if (firstMoment)
                    {
                        p1cooldwn.fillAmount = 0;
                        firstMoment = false;
                    }
                    p1cooldwn.fillAmount += 0.35f / player.GetComponent<PlayerMovementController>().cooldownTime * Time.deltaTime;
                    if (p1cooldwn.fillAmount > 0.99)
                    {
                        p1cooldwn.fillAmount = 0;
                        player.GetComponent<PlayerMovementController>().justAttacked = false;
                    }
                }
                
                else
                {
                    if (firstMoment)
                    {
                        p1cooldwn.fillAmount = 0;
                        firstMoment = false;
                    }
                    p2cooldwn.fillAmount +=
                        0.35f / player.GetComponent<PlayerMovementController>().cooldownTime * Time.deltaTime;
                    if (p2cooldwn.fillAmount > 0.99)
                    {
                        p2cooldwn.fillAmount = 0;
                        player.GetComponent<PlayerMovementController>().justAttacked = false;
                    }
                }
            }
            
        }
        
        
        for (int i = 0; i < puImages.Count; i++)
        {
            if (puImages[i].IsActive())
            {
                puImages[i].fillAmount -= 0.075f*Time.deltaTime;
                if(puImages[i].GetComponent<Image>().fillAmount<0.01)
                {
                    puImages[i].fillAmount = 1f;
                    puImages[i].gameObject.SetActive(false);
                }
            }
        }
        
        
        
    }
}
