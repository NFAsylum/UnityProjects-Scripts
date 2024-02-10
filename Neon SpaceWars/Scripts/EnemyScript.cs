using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D _rb;
    ManagerScript _mScript;

    Transform _pPos;
    bool _movingX, _movingY, stunned;

    public float _originalSpeed = 3, _rotationalSpeed = 5;
    float _diagonalSpeed, _speed;

    public float _health = 10;

    [SerializeField]
    int _xpAmount = 1;

    [SerializeField]
    GameObject xp;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mScript = GameObject.FindWithTag("Manager").GetComponent<ManagerScript>();
        _pPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        _speed = _originalSpeed;
        _diagonalSpeed = _speed * 0.6f;

        StartCoroutine(AutoDestruction());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stunned)
        {
            FollowPlayer();
            LookAtPlayer();
        }
    }

    public void FollowPlayer()
    {
        if (_pPos.position.x == transform.position.x)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        if (_pPos.position.y == transform.position.y)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }


        if (_pPos.position.x - transform.position.x >= 0)
        {
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
        else if (_pPos.position.x - transform.position.x <= 0)
        {
            _movingX = true;
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
        }
        else
        {
            _movingX = false;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        if (_pPos.position.y - transform.position.y <= 0)
        {
            _movingY = true;
            _rb.velocity = new Vector2(_rb.velocity.x, -_speed);
        }
        else if (_pPos.position.y - transform.position.y >= 0)
        {
            _movingY = true;
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);
        }
        else
        {
            _movingY = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }

        if (_movingX && _movingY)
        {
            _speed = _diagonalSpeed;
        }
        else
        {
            _speed = _originalSpeed;
        }
    }

    public void LookAtPlayer()
    {
        float angle = Mathf.Atan2(_pPos.position.y - transform.position.y, _pPos.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationalSpeed);
    }

    IEnumerator TakingDamage(float damage)
    {
        if (_health > damage)
        {
            stunned = true;
            _health -= damage;

            yield return new WaitForSeconds(0.2f);

            stunned = false;
        }
        else
        {
            print(gameObject.name + " destroyed");
            _mScript.currentCount--;
            _mScript.kills++;

            for (int x = 0; x < _xpAmount; x++)
            {
                Transform xpPos = transform;
                xpPos.position = new Vector3(Random.Range(-1, 1f) + xpPos.position.x, Random.Range(-1, 1f) + xpPos.position.y, xpPos.position.z);
                xpPos.localScale *= Random.Range(0.8f, 1.2f);

                GameObject clone = Instantiate(xp, xpPos);
                clone.transform.parent = null;
            }

            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(45);

        _mScript.currentCount--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            coll.gameObject.GetComponent<BulletScript>().resistence--;
            StartCoroutine(TakingDamage(coll.gameObject.GetComponent<BulletScript>().GetDamage()));
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            coll.gameObject.GetComponent<BulletScript>().resistence--;
            StartCoroutine(TakingDamage(coll.gameObject.GetComponent<BulletScript>().GetDamage()));
        }
    }
}
