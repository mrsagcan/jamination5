using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameController, oynaButton, geriButton, hakkindaButtonu;
    [SerializeField] private TextMeshProUGUI hkndText;
    

    // Start is called before the first frame update

    public void startButton()
    {
        gameController.SetActive(true);
        gameObject.SetActive(false);
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
}
