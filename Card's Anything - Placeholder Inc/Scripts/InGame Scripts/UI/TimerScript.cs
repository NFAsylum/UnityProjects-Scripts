using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI _text;

    [SerializeField]
    string[] messages;


    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayTime(float time)
    {
        //print(Mathf.FloorToInt(time));

        float seconds = Mathf.FloorToInt(time);
        _text.text = "Próxima rodada em: " + seconds.ToString();
    }

    public void DisplayEnemy()
    {
        _text.text = messages[Random.Range(0, messages.Length - 1)];
    }
}
