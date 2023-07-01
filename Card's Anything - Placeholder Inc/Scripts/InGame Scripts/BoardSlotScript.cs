using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlotScript : MonoBehaviour
{
    public bool _isOccupied;

    public GameObject _card;
    public Card _cardScript;

    public int _slotIndex;

    public GameObject _aheadSlot;
    GameManagerScript _manager;


    void Start()
    {
        _manager = GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>();

        //_aheadSlot = GameObject.Find("OCardSlot " + _slotIndex);
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (_card == null && _manager._selectedCard != null && _manager._canPlayCard == true)
        {
            _manager._canPlayCard = false;

            _manager.PlayCard(gameObject);

            _isOccupied = true;

            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
