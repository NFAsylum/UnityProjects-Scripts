using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Card : MonoBehaviour
{
    [System.NonSerialized]
    public int _handIndex, _slotOccupiedIndex, _actionPoints = 0;

    //[System.NonSerialized]
    public bool _played = false, _player, _isInspected, _isHovered, _isHighlighted;
    [System.NonSerialized]
    public bool _usedSuperDefenseOneTime, _firstCardBought;

    [System.NonSerialized]
    public Transform _inspectionPos;

    public GameObject highlight, playedActions, handActions, _slot;

    Vector3 _originalPos;
    Quaternion _originalRot;

    //[System.NonSerialized]
    public CardMechanicsScript _actionForThisTurn;

    [System.NonSerialized]
    public int _shield, _upgradeIndex;

    [System.NonSerialized]
    public GameObject _manager;

    Animator _anim;

    [Space]
    [Header("Card Status:")]

    public int id, damageTotal, damageCurrent, healthTotal, healthCurrent, costTotal, costCurrent, upgradeCost, scrapDrop = 1;

    public bool canFuse, isFused;

    public string type, specialCondition;

    public GameObject[] upgradeCard;


    void Start()
    {
        damageCurrent = damageTotal;
        healthCurrent = healthTotal;
        costCurrent = costTotal;

        _originalPos = transform.position;
        _originalRot = transform.rotation;

        _manager = GameObject.FindWithTag("P Game Manager");

        _anim = GetComponent<Animator>();

/*        if (_firstCardBought)
        {
            transform.SetPositionAndRotation(_inspectionPos.position, _inspectionPos.rotation);

            _isInspected = true;
        }*/
    }

    void Update()
    {
        if (_player)
        {
            ResetPosition();
            //CheckUpgradeRecycleSigns();

            if (!_played)
            {
                ZoomInObject();
                HighlightObject();
                //CheckHandActions();
            }
            else
            {
                CheckActions();

                if (_actionForThisTurn == null)
                {
                    for(int x = 0; x < 4; x++)
                    {
                        transform.GetChild(4).transform.GetChild(x).gameObject.SetActive(false);
                    }
                }

/*                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    transform.localPosition = Vector3.zero;
                }*/
            }
        }
    }


    //Card mechanics 

    public void AttackReceived(int incomingDamage)
    {
        int finalDamage = incomingDamage;
        float calcDamage;

        if (_shield == 1)
        {
            calcDamage = incomingDamage / 2;

            if (incomingDamage % 2 != 0 || calcDamage == 0)
            {
                finalDamage = (int)calcDamage + 1;
            }
            else
            {
                finalDamage = (int)calcDamage;
            }
        }
        else if (_shield == 2)
        {
/*            calcDamage = healthTotal / 2;

            if (_usedSuperDefenseOneTime)
            {

            }
            else
            {
                _usedSuperDefenseOneTime = true;
            }

            if (calcDamage % 2 != 0)
            {
                finalDamage = Mathf.FloorToInt(calcDamage) + 1;
            }
            else
            {
                finalDamage = Mathf.FloorToInt(calcDamage);
            }

            calcDamage = healthTotal / 2 + 1;*/
        }

        _shield = 0;
        healthCurrent -= finalDamage;

        print(gameObject.name + " received an attack of " + finalDamage + " damage");

        //Health is less than 0, the card is destroyed and Scrap is received
        if (healthCurrent < 1)
        {
            CardDestroyed();
        }
        else
        {
            _anim.SetTrigger("Attacked");
        }
    }

    void CardDestroyed()
    {
        //print(name + "Destroyed");

        try
        {
            _slot.GetComponent<BoardSlotScript>()._card = null;
            _slot.GetComponent<BoardSlotScript>()._isOccupied = false;
            _slot.GetComponent<BoardSlotScript>()._cardScript = null;

            if (_player)
            {
                _slot.GetComponent<BoxCollider>().enabled = true;
            }
        }
        catch { }

        if (_player)
        {
            _manager.GetComponent<GameManagerScript>()._scrapAmount += scrapDrop;
            _manager.GetComponent<GameManagerScript>().cardsInGame.Remove(this.gameObject);
        }
        else
        {
            GameObject.FindWithTag("O Game Manager").GetComponent<OpponentGameManagerScript>().cardsInGame.Remove(this.gameObject);
        }

        //This block destroy the card (error)
        /*try
        {
            transform.parent = null;
        }
        catch { }*/

        _anim.SetTrigger("Destroyed");

        Destroy(gameObject, 1.1f);
    }

    public void PlayCard(GameObject slot)
    {
        _slot = slot;
        transform.localScale *= 1.5f;

        //print("Played " + gameObject.name);

        GetComponent<Animator>().enabled = true;
        transform.SetPositionAndRotation(slot.transform.position, slot.transform.rotation);
        transform.parent = slot.transform;

        slot.GetComponent<BoardSlotScript>()._card = gameObject;
        slot.GetComponent<BoardSlotScript>()._cardScript = this;

        _isInspected = false;
        _isHighlighted = false;
        _isHovered = false;

        highlight.SetActive(false);

        //transform.localScale *= new Vector2(2.5f, 2.5f);

        try
        {
            if (!_manager.GetComponent<GameManagerScript>()._hasPlayedFirstCard)
            {
                print("Played first card");

                _manager.GetComponent<GameManagerScript>()._hasPlayedFirstCard = true;

                _actionPoints = 1;
            }
        }
        catch { }

        _played = true;
    }

    public void DoActions()
    {
        if (_actionForThisTurn != null)
        {
            _actionForThisTurn.DoAction();
            //print(gameObject.name + " used an action");

            _actionForThisTurn = null;
        }
    }

    public void CheckActions()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_actionPoints > 0)
            {
                if (_isHovered && !_isHighlighted)
                {
                    if (playedActions != null)
                    {
                        playedActions.SetActive(true);
                    }

                    _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = gameObject;
                }
                else if (_isHovered && _actionForThisTurn != null)
                {
                    if (playedActions != null)
                    {
                        playedActions.SetActive(false);
                    }

                    _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = null;
                }
                else if (_actionForThisTurn != null)
                {
                    if (_manager.GetComponent<GameManagerScript>()._inGameSelectedCard == gameObject)
                    {
                        _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = null;
                    }

                    if (playedActions != null)
                    {
                        playedActions.SetActive(false);
                    }
                }
            }
            else if (_isHovered)
            {
                _anim.SetTrigger("CannotAct");
            }
        }
    }

    public void CheckHandActions()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isHovered)
        {
            handActions.SetActive(!handActions.activeSelf);
/*            if (_actionPoints > 0)
            {
                if (_isHovered && !_isHighlighted)
                {
                    if (playedActions != null)
                    {
                        playedActions.SetActive(true);
                    }

                    _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = gameObject;
                }
                else if (_isHovered && _actionForThisTurn != null)
                {
                    if (playedActions != null)
                    {
                        playedActions.SetActive(false);
                    }

                    _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = null;
                }
                else if (_actionForThisTurn != null)
                {
                    if (_manager.GetComponent<GameManagerScript>()._inGameSelectedCard == gameObject)
                    {
                        _manager.GetComponent<GameManagerScript>()._inGameSelectedCard = null;
                    }

                    if (playedActions != null)
                    {
                        playedActions.SetActive(false);
                    }
                }
            }
            else if (_isHovered)
            {
                _anim.SetTrigger("CannotAct");
            }*/
        }
    }


    //Modifications and Scrap

/*    void CheckUpgradeRecycleSigns()
    {
        if (_manager.GetComponent<GameManagerScript>()._scrapView)
        {
            if (_played)
            {
                transform.GetChild(8).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(9).gameObject.SetActive(true);
            }
        }
        else
        {
            transform.GetChild(8).gameObject.SetActive(false);
            transform.GetChild(9).gameObject.SetActive(false);
        }
    }*/

    public void UseScrapForUpgrade()
    {
        //print(gameObject.name + " tried to upgrade");
        try
        {
            if (upgradeCard[_upgradeIndex] != null && upgradeCost <= _manager.GetComponent<GameManagerScript>()._scrapAmount) //&& _manager.GetComponent<GameManagerScript>()._scrapSelectedCard == gameObject)
            {
                //print(gameObject.name + " upgraded to: " + upgradeCard[_upgradeIndex].name);

                _manager.GetComponent<GameManagerScript>()._scrapAmount -= upgradeCost;

                transform.GetChild(4).transform.GetChild(4).gameObject.SetActive(false);

                UpgradedCard(upgradeCard[_upgradeIndex].GetComponent<Card>());

                //Can Destroy, Disable or Change the image and status to reflect upgrade.

                _upgradeIndex++;
            }
            else
            {
                print(gameObject.name + " failed to upgrade due to:");

                if (upgradeCard == null)
                {
                    print("Upgrade Card is null");
                }
                if (upgradeCost > _manager.GetComponent<GameManagerScript>()._scrapAmount)
                {
                    print("Upgrade Cost is superior to scrap amount");
                }
            }
        }
        catch
        {
        }
    }

    public void UpgradedCard(Card newCard)
    {
        if (healthCurrent < healthTotal)
        {
            healthCurrent = newCard.healthTotal - (healthTotal - healthCurrent);
        }
        else
        {
            healthCurrent = newCard.healthCurrent;
        }

        if (damageCurrent > damageTotal && damageCurrent < damageTotal * 2)
        {
            damageCurrent = newCard.damageTotal + (damageCurrent - damageTotal);
        }
        else if (damageCurrent >= damageTotal * 2)
        {
            //damageCurrent++;
            healthCurrent += 2;
        }
        else
        {
            damageCurrent = newCard.damageCurrent;
        }
    }

    public void Recycle()
    {
        int temp = Random.Range(0, 1001);
        
        if (temp < 750)
        {
            scrapDrop = 1;
        }
        else if (temp < 920)
        {
            scrapDrop = 0;
        }
        else
        {
            scrapDrop = 2;
        }

        _shield = 0;

        AttackReceived(1000);
    }


    //Positions, Rotations and Hover

    public Card SetNullCard()
    {
        id = 0;
        damageTotal = 0;
        damageCurrent = 0;
        healthTotal = 0;
        healthCurrent = 0;
        costTotal = 0;
        costCurrent = 0;
        canFuse = false;
        isFused = false;
        type = "null";
        specialCondition = "null";

        return this;
    }

    void OnMouseOver()
    {
        if (_player && !_manager.GetComponent<GameManagerScript>()._gameEnded)
        {
            //print(gameObject.name + " hovered");

            _isHovered = true;

            if (transform.position.y < 0.25f + _originalPos.y && !_isInspected && !_played)
            {
                transform.Translate(new Vector3(0, 2.5f, 0) * Time.deltaTime);
            }
        }
    }

    void OnMouseExit()
    {
        if (_player && !_manager.GetComponent<GameManagerScript>()._gameEnded)
        {
            _isHovered = false;
        }
    }

    public void ResetPosition()
    {
        if (!_isHovered && !_isInspected)
        {
            if (transform.position.y > _originalPos.y)
            {
                transform.Translate(new Vector3(0, -0.9f, 0) * Time.deltaTime);
            }
        }
    }

    public void ZoomInObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _isHovered && !_isInspected)
        {
            transform.SetPositionAndRotation(_inspectionPos.position, _inspectionPos.rotation);

            _isInspected = true;
            //StartCoroutine(ChangeBool(_isSelected));
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && _isHovered)
        {
            transform.SetPositionAndRotation(_originalPos, _originalRot);

            _isInspected = false;
            //StartCoroutine(ChangeBool(_isSelected));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) || (Input.GetKeyDown(KeyCode.Mouse0) && !_isHovered))
        {
            transform.SetPositionAndRotation(_originalPos, _originalRot);
        }
    }

    public void HighlightObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isHovered && !_isHighlighted)
        {
            highlight.SetActive(true);

            _manager.GetComponent<GameManagerScript>()._selectedCard = gameObject;

            _isHighlighted = true;
            //StartCoroutine(ChangeBool(_isHighlighted));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && _isHovered)
        {
            highlight.SetActive(false);

            _manager.GetComponent<GameManagerScript>()._selectedCard = null;

            _isHighlighted = false;
            //StartCoroutine(ChangeBool(_isHighlighted));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_manager.GetComponent<GameManagerScript>()._selectedCard == gameObject)
            {
                _manager.GetComponent<GameManagerScript>()._selectedCard = null;
            }

            highlight.SetActive(false);
        }
    }

    /*IEnumerator ChangeBool(bool var)
    {
        yield return new WaitForSeconds(0.1f);

        var = !var;
    }*/


    void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(GetComponent<BoxCollider>().transform.position, GetComponent<BoxCollider>().size);
        }
        catch { }
    }


    public void EndOfTurn()
    {
        switch (_actionForThisTurn._type)
        {
            case "Attack":
                _actionForThisTurn.Attack();
                break;
            case "Defend":
                _actionForThisTurn.Defend();
                break;
            case "Charge":
                _actionForThisTurn.Charge();
                break;
            case "Super Defense":
                _actionForThisTurn.SuperDefense();
                break;
            case "Upgrade":
                UseScrapForUpgrade();
                break;
        }
    }
}
