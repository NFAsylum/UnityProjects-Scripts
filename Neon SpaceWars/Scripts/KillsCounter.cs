using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillsCounter : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "Kills: " + GameObject.Find("Manager").GetComponent<ManagerScript>().kills;
    }
}
