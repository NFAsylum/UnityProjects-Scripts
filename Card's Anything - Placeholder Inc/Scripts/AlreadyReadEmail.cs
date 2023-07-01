using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlreadyReadEmail : MonoBehaviour
{
    [SerializeField]
    Color normalColor = Color.white, alreadyReadColor = new Color(171, 171, 171, 255);
    [SerializeField]
    string textName;


    void Start()
    {
        string temp = "false";
        try
        {
            temp = PlayerPrefs.GetString(textName);
        }
        catch
        {
            PlayerPrefs.SetString(textName, "false");
            temp = PlayerPrefs.GetString(textName);
        }

        if (temp == "true")
        {
            GetComponent<TextMeshProUGUI>().color = alreadyReadColor;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = normalColor;
            PlayerPrefs.SetInt("emailsNotRead", PlayerPrefs.GetInt("emailsNotRead") + 1);
        }
    }

    public void AlreadyRead()
    {
        if (PlayerPrefs.GetString(textName) != "true")
        {
            PlayerPrefs.SetString(textName, "true");
            PlayerPrefs.SetInt("emailsNotRead", PlayerPrefs.GetInt("emailsNotRead") - 1);
        }

        GetComponent<TextMeshProUGUI>().color = alreadyReadColor;
    }
}
