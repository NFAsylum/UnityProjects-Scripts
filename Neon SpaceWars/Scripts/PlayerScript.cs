using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rb;
    XPColletorScript _xpcScript;
    GunScript _gScript;

    public float _speed = 15, _rotationalSpeed = 5, _health = 3;

    bool _canBeDamaged = true;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _xpcScript = GetComponent<XPColletorScript>();
        _gScript = GetComponent<GunScript>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void Update()
    {
        BuyUpgrade();
    }

    public void PlayerMovement()
    {
        //Velocity

        if (Input.GetKey(KeyCode.W))// && (_rb.velocity.x <= _speed * 2) && (_rb.velocity.y <= _speed * 2))
        {
            //_rb.AddForce(_speed/_rb.mass * transform.up);
            //_rb.AddForce(_speed * transform.up);
            _rb.velocity = _speed * transform.up;
        }

        if (Input.GetKey(KeyCode.S))// && (_rb.velocity.x <= _speed * 2) && (_rb.velocity.y <= _speed * 2))
        {
            //_rb.AddForce(_speed * 2 / _rb.mass * -transform.up);
            //_rb.AddForce(_speed * -transform.up);
            _rb.velocity = _speed * -transform.up * 0.7f;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            _rb.velocity = new Vector2(0, 0);
        }

        //Rotation

        if (_rb.angularVelocity >= -_rotationalSpeed && _rb.angularVelocity <= _rotationalSpeed)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                _rb.AddTorque(_rotationalSpeed * 10);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                _rb.AddTorque(-_rotationalSpeed * 10);
            }
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            _rb.angularVelocity = 0;
        }
    }

    void BuyUpgrade()
    {
        if (_xpcScript.xpAmount > _gScript.upgradeCost && _gScript.canUpgrade && Input.GetKeyDown(KeyCode.U))
        {
            _xpcScript.xpAmount -= _gScript.upgradeCost;
            _gScript.UpgradeGun();
        }
    }

    IEnumerator DamageTaken()
    {
        _canBeDamaged = false;

        _health--;

        if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        yield return new WaitForSeconds(2);

        _canBeDamaged = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && _canBeDamaged)
        {
            StartCoroutine(DamageTaken());
        }
        else if (coll.gameObject.CompareTag("XP"))
        {
            _xpcScript.CollectedXP(coll.gameObject);
            Destroy(coll.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && _canBeDamaged)
        {
            StartCoroutine(DamageTaken());
        }
        else if (coll.gameObject.CompareTag("XP"))
        {
            _xpcScript.CollectedXP(coll.gameObject);
            Destroy(coll.gameObject);
        }
    }
}
