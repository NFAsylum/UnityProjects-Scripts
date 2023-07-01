using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class HealthManagerScript : MonoBehaviour
{
    [SerializeField]
    int _health = 10;

    [SerializeField]
    TextMeshPro _healthCurrentText;
    [SerializeField]
    TextMeshProUGUI _healthCurrentTextUI;

    bool _playerHealth;

    void Start()
    {
        if (_healthCurrentText != null)
        {
            _playerHealth = false;
        }
        else
        {
            _playerHealth= true;
        }
    }

    public void DirectHit(int damage)
    {
        //print("Damage Received");

        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;

            GameObject.FindWithTag("P Game Manager").GetComponent<GameManagerScript>()._gameEnded = true;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (!_playerHealth)
            {
                _healthCurrentText.text = "Vida: " + Convert.ToString(_health);
            }
            else
            {
                _healthCurrentTextUI.text = "Sua vida: " + Convert.ToString(_health);
            }
        }
    }
}
