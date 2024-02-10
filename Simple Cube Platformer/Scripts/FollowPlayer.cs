using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ToFollow();
    }

    public void ToFollow()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

        if(rb.position.x != player.position.x)
        {
            rb.position = new Vector2(player.position.x, rb.position.y);
        }
    }
}
