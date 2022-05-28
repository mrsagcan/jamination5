using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameController, oynaButton, geriButton, hakkindaButtonu, ilkPanel;
    [SerializeField] private TextMeshProUGUI hkndText;
    private bool started;


    [SerializeField] private Image speedImg,smallImg,coolDownImg,speedImg2,smallImg2,coolDownImg2;


    public void startButton()
    {
        gameController.SetActive(true);
        ilkPanel.SetActive(false);
        started = true;
        //player1 = gameController.GetComponent<GameManager>().players[0];
        //player2 = gameController.GetComponent<GameManager>().players[1];
    }

    public void hakkindaButton()
    {
        oynaButton.SetActive(false);
        hkndText.gameObject.SetActive(true);
        geriButton.SetActive(true);
        hakkindaButtonu.SetActive(false);
    }

    public void gerriButton()
    {
        oynaButton.SetActive(true);
        hkndText.gameObject.SetActive(false);
        geriButton.SetActive(false);
        hakkindaButtonu.SetActive(true);
    }

   /*
    * if (speedPowerActivated)
        {
            
            speedImg.fillAmount -= 0.125f*Time.deltaTime;
            if(speedImg.GetComponent<Image>().fillAmount<0.01)
            {
                speedImg.gameObject.SetActive(false);
            }
        } 
        if (smallPowerActivated)
        {
            smallImg.GetComponent<Image>().fillAmount -= 0.125f*Time.deltaTime;
            if(smallImg.GetComponent<Image>().fillAmount<0.01)
            {
                smallImg.gameObject.SetActive(false);
            }
        }
    */
    
}
