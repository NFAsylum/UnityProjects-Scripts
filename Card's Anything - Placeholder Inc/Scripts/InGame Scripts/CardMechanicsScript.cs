using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMechanicsScript : MonoBehaviour
{
    Vector3 _originalScale;

    public string _type;

    bool _hovered;

    public GameObject[] _cardSlots;

    BoardSlotScript _slotScript;

    GameObject _parent;

    int _highlightIndex;


    void Start()
    {
        _originalScale = transform.localScale;

        if (_type == "Upgrade" || _type == "Recycle")
        {
            _parent = transform.parent.gameObject;
        }
        else
        {
            _parent = transform.parent.transform.parent.gameObject;
        }

        switch (_type)
        {
            case "Attack":
                _highlightIndex = 0;
                break;
            case "Defend":
                _highlightIndex = 1;
                break;
            case "SuperDefense":
                _highlightIndex = 2;
                break;
            case "Charge":
                _highlightIndex = 3;
                break;
            case "Upgrade":
                _highlightIndex = 4;
                break;
            case "Recycle":
                _highlightIndex = 5;
                break;
            default:
                print("Type is not recognized");
                break;
        }
    }

    void Update()
    {
        if (_parent.GetComponent<Card>()._actionForThisTurn == this && _type != "Recycle" && _type != "Upgrade")
        {
            transform.localScale = _originalScale * 1.5f;
        }
        else
        {
            transform.localScale = _originalScale;
        }

        if (_parent.GetComponent<Card>()._player && _type == "Upgrade")
        {
            try
            {
                if (_parent.GetComponent<Card>()._manager.GetComponent<GameManagerScript>()._scrapAmount >= _parent.GetComponent<Card>().upgradeCost && _parent.GetComponent<Card>().upgradeCard[_parent.GetComponent<Card>()._upgradeIndex] != null)
                {
                    try
                    {
                        //GameObject temp = _parent.GetComponent<Card>().upgradeCard[_parent.GetComponent<Card>()._upgradeIndex];
                        gameObject.SetActive(true);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            catch
            {
                gameObject.SetActive(false);
            }
        }
    }

    
    public void DoAction()
    {
        //StartCoroutine(AnimatorActionResetDelay());

        switch (_type)
        {
            case "Attack":
                Attack();
                break;
            case "Defend":
                Defend();
                break;
            case "SuperDefense":
                SuperDefense();
                break;
            case "Charge":
                Charge();
                break;
            case "Upgrade":
                _parent.GetComponent<Card>().UseScrapForUpgrade();
                break;
            case "Recycle":
                _parent.GetComponent<Card>().Recycle();
                break;
            default:
                print("Type is not recognized");
                break;
        }
    }

    void DisableAllHighlights()
    {
        for (int i = 0; i < 6; i++)
        {
            _parent.transform.GetChild(4).transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        //print(_parent.name + " attacked");

        _parent.GetComponent<Animator>().SetBool("RealizedAction", true);
        _parent.GetComponent<Animator>().SetInteger("Action", 0);

        _slotScript = _parent.GetComponent<Card>()._slot.GetComponent<BoardSlotScript>();

        if (_slotScript._aheadSlot.GetComponent<BoardSlotScript>()._card != null )
        {
            _slotScript._aheadSlot.GetComponent<BoardSlotScript>()._card.GetComponent<Card>().AttackReceived(_parent.GetComponent<Card>().damageCurrent);
        }
        else
        {
            GameObject.Find("P Game Manager").GetComponent<GameManagerScript>().DirectHit(_parent.GetComponent<Card>().damageCurrent, _parent.GetComponent<Card>()._player);
        }

        //_parent.GetComponent<Animator>().SetBool("RealizedAction", false);
    }

    public void Defend()
    {
        //print(_parent.name + " defended");

        _parent.GetComponent<Animator>().SetBool("RealizedAction", false);
        _parent.GetComponent<Animator>().SetInteger("Action", 0);

        _parent.GetComponent<Card>()._shield = 1;
    }

    public void SuperDefense()
    {
        //print(_parent.name + " used super defense");

        _parent.GetComponent<Animator>().SetBool("RealizedAction", false);
        _parent.GetComponent<Animator>().SetInteger("Action", 0);

        _parent.GetComponent<Card>()._shield = 2;
    }

    public void Charge()
    {
        //print(_parent.name + " used charge");

        _parent.GetComponent<Animator>().SetBool("RealizedAction", false);
        _parent.GetComponent<Animator>().SetInteger("Action", 0);

        _parent.GetComponent<Card>().damageCurrent++;
    }


    public void SelectAction()
    {
        //Returns error for OpponentGameManagerScript call, needs fix
        if (_parent.GetComponent<Card>()._actionPoints > 0 || _type == "Recycle" || _type == "Upgrade")
        {
            //print("Selecting action");

            if (_parent.GetComponent<Card>()._actionForThisTurn == this)
            {
                //print("Null");

                _parent.GetComponent<Card>()._actionForThisTurn = null;

                DisableAllHighlights();
                _parent.GetComponent<Animator>().SetBool("RealizedAction", false);
                _parent.GetComponent<Animator>().SetInteger("Action", 0);
            }
            else
            {
                //print(_type);

                _parent.GetComponent<Card>()._actionForThisTurn = this;

                DisableAllHighlights();
                _parent.transform.GetChild(4).transform.GetChild(_highlightIndex).gameObject.SetActive(true);
                _parent.GetComponent<Animator>().SetInteger("Action", 1);
                _parent.GetComponent<Animator>().SetBool("RealizedAction", false);
            }
        }
        else
        {
            //_parent.GetComponent<Animator>().SetBool("RealizedAction", false);
            _parent.GetComponent<Animator>().SetInteger("Action", 0);
        }
    }


    IEnumerator AnimatorActionResetDelay()
    {
        yield return new WaitForSeconds(0.1f);

        _parent.GetComponent<Animator>().SetInteger("Action", 0);
    }

    void OnMouseOver()
    {
        //print(gameObject.name + " hovered");

        if (_parent.GetComponent<Card>()._player && !GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._gameEnded)
        {
            _hovered = true;
        }
    }

    void OnMouseDown()
    {
        print(gameObject.name + " clicked");

        if (_parent.GetComponent<Card>()._player)
        {
            SelectAction();
        }
    }

    void OnMouseExit()
    {
        if (_parent.GetComponent<Card>()._player)
        {
            transform.localScale = _originalScale;

            _hovered = false;
        }
    }
}
