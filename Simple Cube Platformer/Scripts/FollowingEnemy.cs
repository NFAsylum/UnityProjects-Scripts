using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    public GameObject target; //the enemy's target
    public float moveSpeed = 5; //move speed
    public float rotationSpeed = 5; //speed of turning
    private Rigidbody2D rb;

    public float speed = 3f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        /*        //rotate to look at the player
                transform.rotation = Quaternion.Slerp(transform.rotation.x, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                //move towards the player
                transform.position += transform.forward * Time.deltaTime * moveSpeed;*/


        transform.LookAt(target.transform.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
        if (Vector3.Distance(transform.position, target.transform.position) > 0f)
        {
            //move if distance from target is greater than 1
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }










    /*    public float speed = 4;
        public float attackDistance;
        public float bufferDistance;
        public GameObject player;

        Transform playerTransform;

        void GetPlayerTransform()
        {
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.Log("Player not specified in Inspector");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            GetPlayerTransform();
        }

        // Update is called once per frame
        void Update()
        {
            var distance = Vector3.Distance(playerTransform.position, transform.position);
             Debug.Log("Distance to Player" + distance);
            if (distance <= attackDistance)
            {
                if (distance >= bufferDistance)
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
            }
    */







    /*    private Rigidbody2D _rb;
    public float _speed;
    Vector2 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPos = GameObject.FindWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        float _moveByX = _playerPos.x * _speed;
        float _moveByY = _playerPos.y * _speed;

        _rb.velocity = new Vector2(_moveByX, _moveByY);
    }*/


}
