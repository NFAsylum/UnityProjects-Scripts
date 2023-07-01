using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentGameManagerScript : MonoBehaviour
{
    public List<GameObject> cardsInHands, cardsInDeck, allCards, cardsInGame;

    public GameObject _nullCard;

    public int _maxCards, _currentCards = 0, _initialCards = 3;

    public GameObject _handPos;

    [System.NonSerialized]
    public GameObject _selectedCard, _selectedSlot;

    [SerializeField]
    GameManagerScript _otherManager;

    [SerializeField]
    GameObject[] slots, pSlots;

    [SerializeField]
    bool[] activeSlots;

    [SerializeField]
    //Difficulty change moves, actions and delays
    string _difficulty = "Easy";


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void GameStart(List<GameObject> cardList)
    {
        allCards = cardList;
        cardsInDeck = cardList;

        for (int x = 0; x < _initialCards; x++)
        {
            int i = Random.Range(0, cardsInDeck.Count - 1);

            if (cardsInDeck.Count > 1)
            {
                GetCard(cardsInDeck[i]);

                cardsInDeck.RemoveAt(i);
            }
            else if (!cardsInDeck.Contains(_nullCard))
            {
                GetCard(cardsInDeck[i]);

                cardsInDeck.RemoveAt(i);

                cardsInDeck.Add(_nullCard);
            }
            else
            {
                return;
            }
        }
    }

    public void NextRound()
    {
        //Check for emptly slots
        for (int x = 0; x < activeSlots.Length - 1; x++)
        {
            if (slots[x].GetComponent<BoardSlotScript>()._card == null)
            {
                activeSlots[x] = false;
            }
        }


        //Random card from deck
        int i = Random.Range(0, cardsInDeck.Count - 1);

        if (cardsInDeck.Count > 0 && _currentCards < _maxCards)
        {
            GetCard(cardsInDeck[i]);

            cardsInDeck.RemoveAt(i);
        }

        bool gotCard = false;
        int maxTries = 8, currentTries = 0;

        if (_difficulty == "Hard")
        {

            //Get a good slot to play at.
            while (!gotCard)
            {
                bool cannotFind = false;

                for (int x = 0; x < activeSlots.Length - 1; x++)
                {
                    if (activeSlots[x] == false && pSlots[x].GetComponent<BoardSlotScript>()._card != null)
                    {
                        print("Selected slot " + slots[x] + " because is occupied");

                        activeSlots[x] = true;
                        _selectedSlot = slots[x];

                        PlayCard(_selectedSlot);

                        gotCard = true;
                    }
                    else if (x >= activeSlots.Length - 1)
                    {
                        cannotFind = true;
                    }

                    /*if (activeSlots[x] == false)
                    {
                        activeSlots[x] = true;
                        _selectedSlot = slots[x];

                        //print(_selectedSlot);
                        PlayCard(_selectedSlot);

                        gotCard = true;
                    }*/

                    
                }

                if (cannotFind)
                {
                    //print("Random slot selected");

                    while (!gotCard)
                    {
                        if (currentTries >= maxTries)
                        {
                            gotCard = true;
                        }

                        int x = Random.Range(0, activeSlots.Length - 1);

                        if (activeSlots[x] == false)
                        {
                            activeSlots[x] = true;
                            _selectedSlot = slots[x];

                            //print(_selectedSlot);
                            PlayCard(_selectedSlot);

                            gotCard = true;
                        }

                        currentTries++;
                    }
                }

            }
        }
        else
        {
            //Get random slot to play at.
            while (!gotCard)
            {
                if (currentTries >= maxTries)
                {
                    gotCard = true;
                }

                int x = Random.Range(0, activeSlots.Length - 1);

                if (activeSlots[x] == false)
                {
                    activeSlots[x] = true;
                    _selectedSlot = slots[x];

                    //print(_selectedSlot);
                    PlayCard(_selectedSlot);

                    gotCard = true;
                }

                currentTries++;
            }
        }


        //Actions for cards
        StartCoroutine(ActionsForCards());

        //Starts next round
        StartCoroutine(PlayerTurn());
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

        _currentCards++;
    }


    //Functions using during Game


    public void PlayCard(GameObject pos)
    {
        StartCoroutine(PlayCardWithDelay(pos));

        /*bool gotCard = false;

        while (!gotCard)
        {
            int x = Random.Range(0, cardsInHands.Count);

            if (cardsInHands[x] != null)
            {
                _selectedCard = cardsInHands[x];

                //print(_selectedCard);
                _selectedCard.GetComponent<Card>().PlayCard(pos);

                cardsInHands[_selectedCard.GetComponent<Card>()._handIndex] = null;

                _selectedCard = null;

                _currentCards--;

                gotCard = true;
            }
        }*/
    }

    IEnumerator PlayCardWithDelay(GameObject pos)
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 2f));

        bool gotCard = false;
        int maxTries = 8, currentTries = 0;

        //Needs a function that optimizes card play
        while (!gotCard)
        {
            int x = Random.Range(0, cardsInHands.Count);

            if (cardsInHands[x] != null)
            {
                _selectedCard = cardsInHands[x];

                //print(_selectedCard);
                _selectedCard.GetComponent<Card>().PlayCard(pos);

                cardsInGame.Add(_selectedCard);
                cardsInHands[_selectedCard.GetComponent<Card>()._handIndex] = null;

                _selectedCard = null;

                _currentCards--;

                gotCard = true;
            }
            else
            {
                currentTries++;
            }

            if (currentTries >= maxTries)
            {
                break;
            }
        }
    }

    IEnumerator ActionsForCards()
    {
        yield return new WaitForSeconds(2f);

        for (int x = 0; x < cardsInGame.Count; x++)
        {
            cardsInGame[x].GetComponent<Card>().DoActions();
            cardsInGame[x].GetComponent<Card>()._actionPoints = 1;
        }

        for (int x = 0; x < cardsInGame.Count; x++)
        {
            bool gotAction = false;

            GameObject actions = cardsInGame[x].transform.GetChild(5).gameObject;
            actions.SetActive(true);

            int maxActionTries, currentActionTries = 0;

            if (_difficulty == "Easy")
            {
                yield return new WaitForSeconds(0.001f);
                maxActionTries = 4;

                while (!gotAction)
                {
                    GameObject child = actions.transform.GetChild(Random.Range(0, 4)).gameObject;

                    if (child.activeSelf == true)
                    {
                        //Returns error, needs fix.
                        child.GetComponent<CardMechanicsScript>().SelectAction();

                        gotAction = true;
                    }
                    else if (currentActionTries >= maxActionTries)
                    {
                        gotAction = true;
                    }

                    currentActionTries++;
                }
            }
            else if (_difficulty == "Hard")
            {
                //Needs special cases and more; isn't completed
                yield return new WaitForSeconds(0.05f);
                maxActionTries = 30;

                bool otherSlotHasCard = false;
                otherSlotHasCard = cardsInGame[x].GetComponent<Card>()._slot.GetComponent<BoardSlotScript>()._aheadSlot.GetComponent<BoardSlotScript>()._card != null;

                GameObject atk = actions.transform.GetChild(0).gameObject,
                    def = actions.transform.GetChild(1).gameObject,
                    chr = actions.transform.GetChild(3).gameObject,
                    sup = actions.transform.GetChild(2).gameObject;

                int otherCardHealth = cardsInGame[x].GetComponent<Card>()._slot.GetComponent<BoardSlotScript>()._aheadSlot.GetComponent<BoardSlotScript>()._card.GetComponent<Card>().healthCurrent,
                    otherCardDamage = cardsInGame[x].GetComponent<Card>()._slot.GetComponent<BoardSlotScript>()._aheadSlot.GetComponent<BoardSlotScript>()._card.GetComponent<Card>().damageCurrent,
                    cardHealth = cardsInGame[x].GetComponent<Card>().healthCurrent,
                    cardDamage = cardsInGame[x].GetComponent<Card>().damageCurrent;


                //if slot ahead of current card is null
                if (!otherSlotHasCard)
                {
                    //if can attack and damage is more than 0
                    if (atk.activeSelf == true && cardDamage > 0)
                    {
                        atk.GetComponent<CardMechanicsScript>().SelectAction();
                    }
                    //if can charge
                    else if (chr.gameObject.activeSelf == true)
                    {
                        chr.GetComponent<CardMechanicsScript>().SelectAction();
                    }
                    //if can defend
                    else if (def.activeSelf == true)
                    {
                        def.GetComponent<CardMechanicsScript>().SelectAction();
                    }
                }
                else
                {
                    //if slot is occupied by a card with less health than the current damage
                    if (otherCardHealth <= cardDamage)
                    {
                        //if can attack
                        if (atk.activeSelf == true)
                        {
                            atk.gameObject.GetComponent<CardMechanicsScript>().SelectAction();
                        }
                        else if (def.activeSelf == true)
                        {
                            def.GetComponent<CardMechanicsScript>().SelectAction();
                        }
                    }
                    //if slot is occupied by a card with more damage than the current health
                    else if (otherCardDamage >= cardHealth)
                    {
                        //if can defend
                        if (def.activeSelf == true)
                        {
                            def.GetComponent<CardMechanicsScript>().SelectAction();
                        }
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
                maxActionTries = 10;

                while (!gotAction)
                {
                    GameObject child = actions.transform.GetChild(Random.Range(0, 4)).gameObject;

                    if (child.activeSelf == true)
                    {
                        //Returns error, needs fix.
                        child.GetComponent<CardMechanicsScript>().SelectAction();

                        gotAction = true;
                    }
                    else if (currentActionTries >= maxActionTries)
                    {
                        gotAction = true;
                    }

                    currentActionTries++;
                }
            }





            actions.SetActive(false);
        }
    }

    IEnumerator PlayerTurn()
    {
        if (_difficulty == "Easy")
        {
            yield return new WaitForSeconds(2.2f);
        }
        else
        {
            yield return new WaitForSeconds(3.5f);
        }

        _otherManager._playerTurn = true;

        _otherManager.NextRound();
    }
}
