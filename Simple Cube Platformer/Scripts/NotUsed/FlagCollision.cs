using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollision : MonoBehaviour
{

    public Rigidbody2D rb;

    bool isColliding = false;
    public Transform isCollidingChecker;
    public float checkCollidingRadius;
    public LayerMask collidingLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    public void CheckCollision()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isCollidingChecker.position, checkCollidingRadius, collidingLayer);

        if (colliders != null) 
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }

        if (isColliding == true)
        {
            rb.position = new Vector2(0, 0);
        }
    }
}
