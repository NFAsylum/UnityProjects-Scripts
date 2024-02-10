using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D _rb;

    public float _speed;
    public Transform _oriPosition;
    private Vector2 _currentTarget;

    public Transform _pointA;
    public Transform _pointB;

    public string gameObjTag;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (gameObjTag == "Platform")
        {
            _pointA = GameObject.Find("TargetPointA").GetComponent<Transform>();
            _pointB = GameObject.Find("TargetPointB").GetComponent<Transform>();
        } else if (gameObjTag == "Enemy")
        {
            _pointA = GameObject.Find("EnemyPointA").GetComponent<Transform>();
            _pointB = GameObject.Find("EnemyPointB").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float _speed = 1f;
        var step = _speed * Time.deltaTime;

        if (transform.position == _pointA.position)
        {
            _currentTarget = _pointB.position;
        } else if (transform.position == _pointB.position)
        {
            _currentTarget = _pointA.position;
        }


        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
