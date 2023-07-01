using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class RoundCounterScript : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = Convert.ToString(GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._thisRound);
    }
}
