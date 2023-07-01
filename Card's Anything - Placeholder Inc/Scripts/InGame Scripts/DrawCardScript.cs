using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardScript : MonoBehaviour
{
    GameManagerScript _manager;

    void Start()
    {
        _manager = GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>();
    }

    void OnMouseDown()
    {
        if (_manager._playerTurn && !_manager._playerBoughtCard)
        {
            if (_manager._thisRound <= 1)
            {
                _manager.FirstDraw();
            }
            else
            {
                _manager.NormalDraw();
            }
        }
    }
}
