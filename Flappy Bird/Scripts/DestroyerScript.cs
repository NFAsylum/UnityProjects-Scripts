using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject, 0);
    }
}
