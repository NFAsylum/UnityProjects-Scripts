using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject parent;
    public int numberToSpawn;
    public int limit;
    public float rate;
    
    [System.Serializable]
    public enum RandomRange
    {
        x_minimum, minimum, medium, big, x_big
    }

    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = rate;
    }

    // Update is called once per frame
    void Update()
    {
        ToSpawn();
    }

    public void ToSpawn()
    {
        if (parent.transform.childCount < limit)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                for (int i = 0; i < numberToSpawn; i++)
                {
                    Instantiate(objectToSpawn, new Vector3(this.transform.position.x + GetModifier(), this.transform.position.y + GetModifier())
                        , Quaternion.identity, parent.transform);
                }
                spawnTimer = rate;
            }
        }
    }

    float GetModifier()
    {
        float modifier = Random.Range(0f, 5f);
        if (Random.Range(0, 2) > 0)
            return -modifier;
        else
            return modifier;
    }
}
