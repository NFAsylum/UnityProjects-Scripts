using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkipTurnScript : MonoBehaviour
{
    GameManagerScript _manager;


    void Start()
    {
        _manager = GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>();
    }

    void OnMouseDown()
    {
        if (_manager._playerTurn)
        {
            _manager.EndPlayerTurn();

            transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Enabled");
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Disabled");
        }
    }
}
