using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnTextScript : MonoBehaviour
{
    GameManagerScript _manager;

    TextMeshProUGUI _text;


    void Start()
    {
        _manager = GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>();

        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (_manager._playerTurn)
        {
            _text.text = "Turno: Jogador";
        }
        else
        {
            _text.text = "Turno: Inimigo";
        }
    }
}
