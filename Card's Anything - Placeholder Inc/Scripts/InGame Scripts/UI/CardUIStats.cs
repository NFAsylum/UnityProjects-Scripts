using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;
using System;

public class CardUIStats : MonoBehaviour
{
    GameObject _parent;
    TextMeshPro _damageText, _healthText;

    [SerializeField]
    Color32[] colors;


    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.gameObject;

        _damageText = transform.GetChild(0).GetComponent<TextMeshPro>();
        _healthText = transform.GetChild(1).GetComponent<TextMeshPro>();

        //UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        //Damage Text
        _damageText.text = Convert.ToString(_parent.GetComponent<Card>().damageCurrent);

        //Health Text

        if (_parent.GetComponent<Card>().healthCurrent <= 0)
        {
            _healthText.text = Convert.ToString(0);
        }
        else
        {
            _healthText.text = Convert.ToString(_parent.GetComponent<Card>().healthCurrent);
        }
    }

    void UpdateColorStats()
    {
        //Damage Text
        _damageText.text = Convert.ToString(_parent.GetComponent<Card>().damageCurrent);

        if (_parent.GetComponent<Card>().damageCurrent > 0)
        {
            if (_parent.GetComponent<Card>().damageCurrent == _parent.GetComponent<Card>().damageTotal)
            {
                _damageText.color = colors[0]; 
                //new Color32(255/ 224, 255 / 16, 255 / 16, 1);
            }
            else if (_parent.GetComponent<Card>().damageCurrent > _parent.GetComponent<Card>().damageTotal)
            {
                _damageText.color = colors[1]; 
                //new Color32(255 / 150, 255 / 17, 255 / 17, 1);
            }
            else if (_parent.GetComponent<Card>().damageCurrent < _parent.GetComponent<Card>().damageTotal)
            {
                _damageText.color = colors[2]; 
                //new Color32(255 / 217, 255 / 69, 255 / 69, 1);
            }
        }
        else
        {
            _damageText.color = Color.white;
        }


        //Health Text

        if (_parent.GetComponent<Card>().healthCurrent <= 0)
        {
            _healthText.text = Convert.ToString(0);
            _healthText.color = Color.white;
        }
        else
        {
            if (_parent.GetComponent<Card>().healthCurrent == _parent.GetComponent<Card>().healthTotal)
            {
                _healthText.color = colors[0];
                    //new Color32(255 / 224, 255 / 16, 255 / 16, 1);
            }
            else if (_parent.GetComponent<Card>().healthCurrent < _parent.GetComponent<Card>().healthTotal)
            {
                _healthText.color = colors[1]; 
                //new Color32(255 / 217, 255 / 69, 255 / 69, 1);
            }
            else if (_parent.GetComponent<Card>().healthCurrent > _parent.GetComponent<Card>().healthTotal)
            {
                _healthText.color = colors[2]; 
                //new Color32(255 / 150, 255 / 17, 255 / 17, 1);
            }

            _healthText.text = Convert.ToString(_parent.GetComponent<Card>().healthCurrent);
        }
    }
}
