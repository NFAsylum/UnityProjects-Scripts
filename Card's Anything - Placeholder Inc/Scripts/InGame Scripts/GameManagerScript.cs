using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public List<GameObject> cardsInHands, cardsInDeck, allCards, cardsInGame;

    public GameObject _nullCard;

    public int _maxCards, _initialCards, _currentCards = 0, _initialPoints, _thisRound, _scrapAmount = 0;

    public GameObject _handPos, _cardPlayStatusObj;

    [SerializeField]
    Sprite[] _cardPlayStatusImgs;

    [System.NonSerialized]
    public GameObject _selectedCard, _inGameSelectedCard, _scrapSelectedCard;

    public GameObject _inspectionPos;

    [SerializeField]
    OpponentGameManagerScript _otherManager;

    [SerializeField]
    HealthManagerScript _healthManager, _OHealthManager;

    [System.NonSerialized]
    public bool _gameEnded, _playerTurn = true, _canPlayCard = true, _playerBoughtCard = false, _hasPlayedFirstCard, _hasBoughtFirstCard, _scrapView;
    [SerializeField]
    bool _hasTime;

    [System.NonSerialized]
    public float _remainingTime, _timeValue = 45;

    [SerializeField]
    Animator _turnIndicator;


    void Start()
    {
        _remainingTime = _timeValue * 2f;
    }

    void Update()
    {
        if (_playerTurn && !_gameEnded)
        {
            //print(Mathf.FloorToInt(_remainingTime % 60));

            if (_hasTime)
            {
                if (_remainingTime > 0)
                {
                    _remainingTime -= Time.deltaTime;
                }
                else
                {
                    EndPlayerTurn();

                    _remainingTime = _timeValue;
                }

                GameObject.Find("Timer").GetComponent<TimerScript>().DisplayTime(_remainingTime);
            }
        }
        else if (_hasTime)
        {
            GameObject.Find("Timer").GetComponent<TimerScript>().DisplayEnemy();
        }
        else if (_gameEnded)
        {
            GameObject.Find("Manager").GetComponent<ButtonFunctionsScript>().StartScene("Game3 Scene");
        }
    }

    public void GameStart(List<GameObject> cardList)
    {
        allCards = cardList;
        cardsInDeck = cardList;

        print("Starting Game");

        _thisRound++;
    }

    public void FirstDraw()
    {
        for (int x = 0; x < _initialCards; x++)
        {
            int i = Random.Range(0, cardsInDeck.Count - 1);

            if (cardsInDeck.Count > 1)
            {
                GetCard(cardsInDeck[i]);

                //print(string.Format("Added to hands: {0}", cardsInDeck[i]));

                cardsInDeck.RemoveAt(i);
            }
            else if (!cardsInDeck.Contains(_nullCard))
            {
                GetCard(cardsInDeck[i]);

                //print(string.Format("Added to hands: {0}", cardsInDeck[i]));

                cardsInDeck.RemoveAt(i);

                cardsInDeck.Add(_nullCard);
            }
            else
            {
                //print(string.Format("Added to hands: {0}", cardsInDeck[i]));

                return;
            }
        }

        _playerBoughtCard = true;
    }

    public void NextRound()
    {
        _turnIndicator.SetTrigger("Change");
        _cardPlayStatusObj.GetComponent<Image>().sprite = _cardPlayStatusImgs[0];

        _canPlayCard = true;

        for (int x = 0; x < cardsInGame.Count; x++)
        {
            cardsInGame[x].GetComponent<Card>()._actionPoints = 1;
        }

        _thisRound++;

        _playerBoughtCard = false;
    }

    public void NormalDraw()
    {
        //Random card from deck
        int i = Random.Range(0, cardsInDeck.Count - 1);

        if (cardsInDeck.Count > 0 && _currentCards < _maxCards)
        {
            GetCard(cardsInDeck[i]);

            //print(string.Format("Added to hands: {0}", cardsInDeck[i]));

            cardsInDeck.RemoveAt(i);
        }
        else
        {
            print("Deck is over");
        }

        _playerBoughtCard = true;
    }

    public void GetCard(GameObject card)
    {
        int handIndex = 0;
        int handSlotIndex = 0;

        for (int x = 0; x < _handPos.transform.childCount; x++)
        {
            try
            {
                if (_handPos.transform.GetChild(x) == null)
                {
                    handSlotIndex = x;
                    break;
                }
            }
            catch
            {
                handSlotIndex = x;
                break;
            }
        }


        for (int x = 0; x < cardsInHands.Count; x++)
        {
            try
            {
                if (cardsInHands[x].name == null)
                {
                    handIndex = x;
                    break;
                }
            }
            catch
            {
                handIndex = x;
                break;
            }
        }

        GameObject clone = Instantiate(card, _handPos.transform.GetChild(handIndex));

        cardsInHands[handIndex] = clone;

        clone.GetComponent<Card>()._handIndex = handIndex;
        clone.GetComponent<Card>()._player = true;
        clone.GetComponent<Card>()._inspectionPos = _inspectionPos.transform;

        _currentCards++;

        if (!_hasBoughtFirstCard)
        {
            _hasBoughtFirstCard = true;

            //clone.GetComponent<Card>()._firstCardBought = true;
        }
    }


    //Functions using during Game


    public void PlayCard(GameObject pos)
    {
        if (_playerTurn)
        {
            _selectedCard.GetComponent<Card>().PlayCard(pos);

            cardsInHands[_selectedCard.GetComponent<Card>()._handIndex] = null;

            cardsInGame.Add(_selectedCard);

            _selectedCard = null;

            _currentCards--;

            _cardPlayStatusObj.GetComponent<Image>().sprite = _cardPlayStatusImgs[1];
        }
    }

    public void EndPlayerTurn()
    {
        _remainingTime = _timeValue;

        for (int x = 0; x < cardsInGame.Count; x++)
        {
            cardsInGame[x].GetComponent<Card>().DoActions();
            cardsInGame[x].GetComponent<Card>()._actionPoints = 1;
        }

        for (int x = 0; x < cardsInHands.Count; x++)
        {
            try
            {
                cardsInHands[x].GetComponent<Card>().DoActions();
            }
            catch { }
        }


        _otherManager.NextRound();

        _turnIndicator.SetTrigger("Change");
        _cardPlayStatusObj.GetComponent<Image>().sprite = _cardPlayStatusImgs[1];

        _playerTurn = false;
    }

    public void EndEnemyTurn()
    {
        NextRound();
    }

    public void DirectHit(int damage, bool attackEnemy)
    {
        if (attackEnemy)
        {
            _OHealthManager.DirectHit(damage);
        }
        else
        {
            _healthManager.DirectHit(damage);
        }
    }

    public void ShowHideUpgradeRecycleCards(bool value)
    {
        //print("Called ShowHideUpgradeRecycleCards");

        for (int x = 0; x < cardsInGame.Count; x++)
        {
            try
            {
                if (cardsInGame[x].GetComponent<Card>().upgradeCard != null)
                {
                    cardsInGame[x].transform.GetChild(8).gameObject.SetActive(value);
                    cardsInGame[x].transform.GetChild(9).gameObject.SetActive(false);
                }
            }
            catch { }
        }
                
        for (int x = 0; x < cardsInHands.Count; x++)
        {
            try
            {
                cardsInHands[x].transform.GetChild(9).gameObject.SetActive(value);
                cardsInHands[x].transform.GetChild(8).gameObject.SetActive(false);
            }
            catch { }
        }
         
    }


    //Functions used for debug purposes


    public void CheckHands()
    {
        for (int x = 0; x < cardsInHands.Count; x++)
        {
            if (cardsInHands[x] != null)
            {
                print(cardsInHands[x].name);
            }
        }
    }

/*    public void DestroyCardsInHand()
    {
        int x = _handPos.transform.childCount;

        while (x > 0)
        {
            try
            {
                cardsInDeck.Remove(_handPos.transform.GetChild(x++).transform.GetChild(0).gameObject);
                Destroy(_handPos.transform.GetChild(x++).transform.GetChild(0).gameObject);
                x--;
                _currentCards--;
            }
            catch
            {
                print(string.Format("Error during destroying card in {0}", _handPos.transform.GetChild(x++).gameObject.name));
            }
        }
    }

    public void DestroyCard()
    {
        if (_currentCards > 0)
        {
            _currentCards--;

            print(_handPos.transform.GetChild(_currentCards).transform.GetChild(0).gameObject);
            cardsInHands.RemoveAt(_handPos.transform.GetChild(_currentCards).transform.GetChild(0).gameObject.GetComponent<Card>()._handIndex);
            Destroy(_handPos.transform.GetChild(_currentCards).transform.GetChild(0).gameObject);
        }
        else
        {
            print("There aren't cards to destroy");
        }
    }*/
}
