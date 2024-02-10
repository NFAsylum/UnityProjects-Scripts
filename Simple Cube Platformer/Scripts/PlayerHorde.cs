using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorde : MonoBehaviour
{

    private Rigidbody2D rb;
    public Collider2D collision;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float moveByX = x * speed;
        float moveByY = y * speed;

        rb.velocity = new Vector2(moveByX, moveByY);

    }

    public void OnCollisionEnter (Collision2D collision)
    {
        print("Collision");
        if(collision.collider == GameObject.FindWithTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
