using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI _text;

    float elapsedTime, seconds, minutes, hours;


    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        else
        {
            seconds += Time.deltaTime;
        }

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }

        if (hours >= 24)
        {
            print("Completed a day");
            hours = 0;
            elapsedTime = 0;
        }

        if (Mathf.RoundToInt(seconds) < 10 && Mathf.RoundToInt(minutes) < 10 && Mathf.RoundToInt(hours) < 10)
        {
            _text.text = string.Format("0{0,0}:0{1,0}:0{2,0}", Mathf.RoundToInt(hours), Mathf.RoundToInt(minutes), Mathf.RoundToInt(seconds));
        }
        else if (Mathf.RoundToInt(minutes) < 10 && Mathf.RoundToInt(hours) < 10)
        {
            _text.text = string.Format("0{0,0}:0{1,0}:{2,0}", Mathf.RoundToInt(hours), Mathf.RoundToInt(minutes), Mathf.RoundToInt(seconds));
        }
        else if (Mathf.RoundToInt(hours) < 10)
        {
            _text.text = string.Format("0{0,0}:{1,0}:{2,0}", Mathf.RoundToInt(hours), Mathf.RoundToInt(minutes), Mathf.RoundToInt(seconds));
        }
        else
        {
            _text.text = string.Format("{0,0}:{1,0}:{2,0}", Mathf.RoundToInt(hours), Mathf.RoundToInt(minutes), Mathf.RoundToInt(seconds));
        }
    }
}
