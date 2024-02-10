using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPColletorScript : MonoBehaviour
{
    public float xpAmount = 0;

    public void CollectedXP(GameObject xp)
    {
        xpAmount += xp.GetComponent<XPScript>().value;
    }
}
