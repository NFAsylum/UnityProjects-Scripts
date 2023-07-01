using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueEventScript : MonoBehaviour
{
    /*public GameObject[] _blockedAreas;
    public bool _blockArea, _playingEvent, _ended, _immediate;
    public Color[] _textColor;
    public Sprite[] _characterDisplayed;
    public float[] _spellingSpeed;
    public float _nextMessageDelay;
    public string _eventType;
    public string[] messages;*/

/*    [Header("EVENTS TYPES: \nAutomaticDialogue;")]
    [Space]*/

    public string _textType;

    public float _finalMessageDelay;
    public float[] _betweenMessagesDelay;

    public bool _skippable, _interactable, _immediate, _playingEvent, _blockArea;
    [System.NonSerialized]
    public bool _ended;

    public GameObject[] _blockedAreas;

    GameObject clone;

    [Header("DIALOGUE EVENT \nWARNING: ARRAYS MUST MATCH IN SIZE.")]
    [Space]

    public Sprite[] _characterDisplayed;

    public string[] messages;

    [Header("Default: 0.05; \nThe higher, the slower.")]
    public float[] _spellingSpeed;

    [Header("Min(black) = 0, Max(white) = 1")]
    public Color[] _textColor;

    [Space]
    [Space]
    [Space]

    [Header("Camera Movement")]
    [Space]

    public bool _moveCamera;
    bool _finalized;

    public CinemachineVirtualCamera[] _camera;


    void Start()
    {
        GameObject.FindWithTag("DialogueBox").GetComponent<DialogueWrittingScript>().Event(gameObject);
    }
}
