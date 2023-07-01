using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapScript : MonoBehaviour
{
    bool _usingScrapView, _clicked;

    [SerializeField]
    GameObject _scrapViewPanel;

    void Update()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = Convert.ToString(GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._scrapAmount);

        if (Input.GetKeyDown(KeyCode.U))
        {
            _clicked = true;
        }

        ScrapView();
    }

    public void ScrapView()
    {
        if (_clicked)
        {
            _clicked = false;

            _usingScrapView = !_usingScrapView;

            if (_usingScrapView)
            {
                _scrapViewPanel.SetActive(true);

                GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._scrapView = true;
                GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>().ShowHideUpgradeRecycleCards(true);
            }
            else
            {
                _scrapViewPanel.SetActive(false);

                GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._scrapView = false;
                GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>().ShowHideUpgradeRecycleCards(false);
            }
        }

    }

    void OnMouseDown()
    {
        _clicked = true;
    }
}
