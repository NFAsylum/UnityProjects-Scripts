using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public GameObject _pipes;
    public float _pipeSpeed = 4;
    public float _pipeDelay = 2;
    float _pipeElapsed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PipeStart();
    }

    public void PipeStart()
    {
        _pipeElapsed += Time.deltaTime;
        try
        {
            if (_pipeElapsed >= _pipeDelay)
            {
                GameObject clone = GameObject.Instantiate(_pipes, GameObject.Find("Scenario Start").transform.position, Quaternion.identity);
                clone.GetComponent<Rigidbody2D>().velocity = -(GameObject.Find("Scenario Start").transform.right * _pipeSpeed);

                Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y + 0.01f);

                if (_pipeSpeed < 20)
                {
                    _pipeSpeed += 0.01f;
                }
                if (_pipeDelay > 0.5)
                {
                    _pipeDelay -= 0.01f;
                }

                _pipeElapsed = 0;
            }
        }
        catch (MissingReferenceException)
        {
            
        }
    }
}
