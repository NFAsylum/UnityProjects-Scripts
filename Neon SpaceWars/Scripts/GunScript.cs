using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    GameObject projectile, shotPosition;

    [SerializeField]
    GunScript[] gunUpgrades;

    [SerializeField]
    float speed = 60, delay = 0.4f;
    float initialDelay;

    public float upgradeCost = 5;

    bool canShot = true;
    public bool canUpgrade = true;


    void Start()
    {
        initialDelay = delay;
    }

    void Update()
    {
        if (canShot)
        {
            StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        canShot = false;

        GameObject shot = Instantiate(projectile, shotPosition.transform);
        shot.transform.up = transform.up;
        shot.GetComponent<Rigidbody2D>().velocity = speed * shot.transform.up;
        shot.transform.parent = null;

        yield return new WaitForSeconds(delay);

        canShot = true;
    }

    public void UpgradeGun()
    {
        if (canUpgrade)
        {
            if (delay <= initialDelay / 2)
            {
                canUpgrade = false;

                if (gunUpgrades != null)
                {
                    gunUpgrades[Random.Range(0, gunUpgrades.Length)].enabled = true;
                }
            }
            else
            {
                delay *= 0.9f;
                upgradeCost *= 1.5f;
            }
        }
    }
}
