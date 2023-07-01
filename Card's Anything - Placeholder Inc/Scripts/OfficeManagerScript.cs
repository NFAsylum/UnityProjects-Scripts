using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OfficeManagerScript : MonoBehaviour
{
    //public bool readSpecialEmail;
    [SerializeField]
    GameObject connectClickHandler, connectIcon;

    [SerializeField]
    Sprite hackerImage;
    Sprite connectImage;

    public bool debuggingGame = false;

    [SerializeField]
    GameObject hackedEmail, normalEmail, blackScreen, loadingScreen;


    // Start is called before the first frame update
    void Start()
    {
        if (debuggingGame)
        {
            PlayerPrefs.DeleteAll();
        }

        connectImage = connectIcon.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FinishOffice());
        }
    }

    public void TurnOnConnect()
    {
        connectClickHandler.SetActive(true);
        connectIcon.GetComponent<Image>().sprite = hackerImage;
    }

    public void HackedConnect()
    {
        connectClickHandler.SetActive(false);
        connectIcon.GetComponent<Image>().sprite = connectImage;

        //hackedEmail.SetActive(true);
        normalEmail.SetActive(false);
        PlayerPrefs.SetInt("emailsNotRead", PlayerPrefs.GetInt("emailsNotRead") + 1);
    }

    public void FinishOfficeScene()
    {
        StartCoroutine(FinishOffice());
    }

    IEnumerator FinishOffice()
    {
        yield return new WaitForSeconds(3f);

        blackScreen.SetActive(true);
        GetComponent<ButtonFunctionsScript>().transition.SetActive(true);

        yield return new WaitForSeconds(3);

        loadingScreen.SetActive(true);

        GetComponent<ButtonFunctionsScript>().StartScene("Game Scene");
    }
}
