using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] _enemy;
    Transform _player;

    [System.NonSerialized]
    public float _delay = 4;
    float _elapsedTime, minDistance = 15, maxDistance = 60;

    int _enemyIndex = 0;

    [SerializeField]
    bool minDistanceIsOK, maxDistanceIsOK;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    
    void Update()
    {
        if (Mathf.Abs(_player.position.x - transform.position.x) > minDistance || Mathf.Abs(_player.position.y - transform.position.y) > minDistance)
        {
            minDistanceIsOK = true;
        }
        else
        {
            minDistanceIsOK = false;
        }

        if (Mathf.Abs(_player.position.x - transform.position.x) < maxDistance && Mathf.Abs(_player.position.y - transform.position.y) < maxDistance)
        {
            maxDistanceIsOK = true;
        }
        else
        {
            maxDistanceIsOK = false;
        }

        if (minDistanceIsOK && maxDistanceIsOK && GameObject.Find("Manager").GetComponent<ManagerScript>().currentCount < GameObject.Find("Manager").GetComponent<ManagerScript>().maxCount)
        {
            SpawnEnemy();
        }

        //EnemyLevelUp();
    }

    public void SpawnEnemy()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _delay)
        {
            GameObject.Find("Manager").GetComponent<ManagerScript>().currentCount++;

            Instantiate(_enemy[Random.Range(0, _enemyIndex + 1)], transform);
            _elapsedTime = 0;
        }
    }

    public void EnemyLevelUp()
    {
        try
        {
            if (_enemy[_enemyIndex++] != null && Time.time > 15 * (_enemyIndex + 1))
            {
                _enemyIndex += 1;
            }
        }
        catch { }
    }
}
