using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EOPScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float _rd = Random.Range(-2, 3);
        transform.position = new Vector2(transform.position.x, _rd);
    }
}
