using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    Rigidbody2D _rb;

    public float _speed;

    public int _points;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector2(0, _speed);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject, 0);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "End of Pipes")
        {
            _points += 1;
            print(_points);
            _speed += 0.1f;
        }
    }
}
