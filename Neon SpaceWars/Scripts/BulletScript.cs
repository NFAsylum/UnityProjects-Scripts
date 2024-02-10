using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float damage = 5, criticalChance = 5;
    public float resistence = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (resistence <= 0)
        {
            Destroy(gameObject);
        }   
    }

    public float GetDamage()
    {
        float finalDamage = damage;
        return finalDamage;
    }
}
