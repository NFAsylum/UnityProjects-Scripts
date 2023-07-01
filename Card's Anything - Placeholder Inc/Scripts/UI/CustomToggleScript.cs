using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomToggleScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Sprite on, off;

    [SerializeField]
    GameObject target;

    [SerializeField]
    string action;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (action == "SetFullScreen")
        {
            if (transform.GetChild(0).gameObject.GetComponent<Image>().sprite == on)
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = off;

                target.GetComponent<OptionsScript>().SetFullScreen(false);

                //print("FullScreen set to false");
            }
            else
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().sprite = on;

                target.GetComponent<OptionsScript>().SetFullScreen(true);

                //print("FullScreen set to true");
            }
        }
    }
}
