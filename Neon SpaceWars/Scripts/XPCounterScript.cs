using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPCounterScript : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "XP: " + GameObject.FindWithTag("Player").GetComponent<XPColletorScript>().xpAmount;
    }
}
