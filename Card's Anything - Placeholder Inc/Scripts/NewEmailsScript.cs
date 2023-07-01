using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewEmailsScript : MonoBehaviour
{
    [SerializeField]
    Sprite newEmails, noNewEmails;

    bool canStart;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            print(PlayerPrefs.GetInt("emailsNotRead"));
            int temp = PlayerPrefs.GetInt("emailsNotRead");

            if (temp == 0)
            {
                GetComponent<Image>().sprite = noNewEmails;
            }
            else
            {
                GetComponent<Image>().sprite = newEmails;
            }
        }
    }

    public void StartProcess()
    {
        canStart = true;
    }
}
