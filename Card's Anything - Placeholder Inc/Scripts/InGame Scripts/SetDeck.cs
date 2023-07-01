using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeck : MonoBehaviour
{
    //public int[] cardsIds;

    [Header("Deck shouldn't be smaller than initial cards")]
    [SerializeField]
    List<GameObject> cardsDeck;

    [SerializeField]
    bool _isPlayer;

    [SerializeField]
    GameObject _manager;


    void Start()
    {
        //CardList cardList = GameObject.Find("Game Manager").GetComponent<CardList>();
        /*for (int x = 1; x < cardsIds.Length; x++)
        {
            cardsDeck[x] = cardList.GetCardById(cardsIds[x]);
        }*/

        if (_isPlayer)
        {
            _manager.GetComponent<GameManagerScript>().GameStart(cardsDeck);
        }
        else
        {
            _manager.GetComponent<OpponentGameManagerScript>().GameStart(cardsDeck);
        }
    }

    public void NextRoundCommand()
    {
        if (_isPlayer)
        {
            _manager.GetComponent<GameManagerScript>().GameStart(cardsDeck);
        }
        else
        {
            _manager.GetComponent<OpponentGameManagerScript>().GameStart(cardsDeck);
        }
    }
}
