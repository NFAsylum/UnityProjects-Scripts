using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Platformer : MonoBehaviour
{

    public Rigidbody2D rb;

    public float speed;

    public float jumpForce;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float rememberGroundedFor;
    float lastTimeGrounded;

    private float respawnX;
    private float respawnY;

    public GameObject coll;

    public GameObject target;

    public GameObject flag;

    public GameObject enemy;
    private int lostLife;
    public int lifes;

    public Text points;
    public int intPoints;

    public int defaultAdditionalJumps = 1;
    public int additionalJumps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        CheckIfGrounded();
        Restart();
    }

    public void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded ||
            Time.time - lastTimeGrounded <= rememberGroundedFor ||
            additionalJumps > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
            rb.rotation = 0;
        }
    }

    public void Restart()
    {
        respawnX = GameObject.FindWithTag("Respawn").GetComponent<Transform>().position.x;
        respawnY = GameObject.FindWithTag("Respawn").GetComponent<Transform>().position.y;

        if (Input.GetKeyDown(KeyCode.R) || lostLife == 1)
        {
            rb.position = new Vector2(respawnX, respawnY);
            rb.rotation = 0;
            rb.velocity = new Vector2(0, 0);

            if (lostLife == 1)
            {
                lifes--;
                lostLife = 0;
            }

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //print("Collision detected");

        target = GameObject.FindWithTag("Target");

        enemy = GameObject.FindWithTag("Enemy");

        flag = GameObject.Find("Flag");

        coll = collision.collider.gameObject;

        if (coll == target)
        {
            points = GameObject.Find("PointsCounter").GetComponent<Text>();
            intPoints = int.Parse(points.text);
            intPoints++;
            points.text = Convert.ToString(intPoints);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            rb.position = new Vector2(respawnX, respawnY);
        } else if (coll == enemy && lifes == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (coll == enemy)
        {
            lostLife = 1;
        }
    }
}
