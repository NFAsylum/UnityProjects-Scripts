using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHordePlayer : MonoBehaviour
{

    public Vector2 playerPos;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerPos = GameObject.Find("HordePlayer").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        ToFollow();
    }

    public void ToFollow()
    {
        playerPos = GameObject.Find("HordePlayer").GetComponent<Transform>().position;

        if (playerPos.x != rb.position.x || playerPos.y != rb.position.y)
        {
            rb.position = playerPos;
        }
    }
}
