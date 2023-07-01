using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DialogueWrittingScript : MonoBehaviour
{
    bool _isOpenMenu, _cutscene, _onEvent, _skippable, _skip, _spelling, _toNarrate, _isNarrating, _immediate;

    public GameObject textObjRegular, textObjBold, textObjLight, textObjItalic;
    GameObject clone, _eventSrc, finalTextObj;
    DialogueEventScript _srcScript;

    Color[] _textColor;

    Sprite[] _characterDisplayed;

    string _eventType;

    float[] _spellingSpeed;

    int _sceneIndex, _msgsLength, _msgsIndex = 0, _letterIndex = 0, _clones;


    void Start()
    {
        
    }


    void Update()
    {
        if (_onEvent)
        {
            if (_srcScript._moveCamera)
            {
                /*for (int x = 0; x < _srcScript._camera.Length; x++)
                {
                    _srcScript._camera[x].Priority = x + 6;
                }*/
            }

/*            if (_eventType == "AutomaticDialogue")
            {
                AutomaticDialogueEvent();
            }*/

            AutomaticDialogueEvent();
        }
    }

    public void Event(GameObject source)
    {
        _eventSrc = source;
        _srcScript = _eventSrc.GetComponent<DialogueEventScript>();
        //_skippable = _srcScript._skippable;
        _textColor = _srcScript._textColor;
        _spellingSpeed = _srcScript._spellingSpeed;
        //_characterDisplayed = _srcScript._characterDisplayed;
        _immediate = _srcScript._immediate;

        _msgsLength = _srcScript.messages.Length;

        _onEvent = true;

        transform.GetChild(0).gameObject.SetActive(true);

        switch (_srcScript._textType)
        {
            case "Regular":
                finalTextObj = textObjRegular;
                break;
            case "Bold":
                finalTextObj = textObjBold;
                break;
            case "Light":
                finalTextObj = textObjLight;
                break;
            case "Italic":
                finalTextObj = textObjItalic;
                break;
            default:
                finalTextObj = textObjRegular;
                break;
        }
    }

    public void AutomaticDialogueEvent()
    {
        if (_msgsIndex < _msgsLength && !_spelling)
        {
            StartCoroutine(TextWritting());
        }
        else if (_msgsIndex >= _msgsLength)
        {
            if (_srcScript._blockArea)
            {
                for (int x = 0; x < _srcScript._blockedAreas.Length; x++)
                {
                    print("Deactivated " + _srcScript._blockedAreas[x]);
                    _srcScript._blockedAreas[x].SetActive(false);
                }
            }

            StopCoroutine(TextWritting());

            _srcScript._playingEvent = false;
            _srcScript._ended = true;

            if (_srcScript._moveCamera)
            {
                for (int x = 0; x < _srcScript._camera.Length; x++)
                {
                    _srcScript._camera[x].Priority = 0;
                }
            }

            StartCoroutine(ResetStats());
        }
    }

    public IEnumerator TextWritting()
    {
        _spelling = true;

        //Transform pos = transform;
        //pos.position = new Vector2(transform.position.x - 10, transform.position.y);

/*        if (clone == null)
        {
*//*           if (_toNarrate)
            {
                clone = Instantiate(chatBoxNarrator, pos);

            }
            else
            {
                clone = Instantiate(chatBox, pos);
            }
        }*/

        print("Start dialogue");
        finalTextObj.GetComponent<TextMeshProUGUI>().text = null;

        finalTextObj.GetComponent<TextMeshProUGUI>().color = _textColor[_msgsIndex];

        /*        if (_characterDisplayed[_msgsIndex] != null)
                {
                    chatBox.transform.GetChild(0).GetComponent<Image>().sprite = _characterDisplayed[_msgsIndex];
                }*/
        
        for (int x = 0; x < _srcScript.messages[_msgsIndex].Length; x++)
        {
            print(_srcScript.messages[_msgsIndex].Length);
            finalTextObj.GetComponent<TextMeshProUGUI>().text += _srcScript.messages[_msgsIndex][x];

            if (!_immediate)
            {
                yield return new WaitForSeconds(_spellingSpeed[_msgsIndex]);
            }
        }
        print("End dialogue");
        //}

        yield return new WaitForSeconds(_srcScript._betweenMessagesDelay[_msgsIndex]);

        _msgsIndex++;
        _spelling = false;
    }

    IEnumerator ResetStats()
    {
        yield return new WaitForSeconds(_srcScript._finalMessageDelay);

        _onEvent = false;

        _eventSrc = null;
        _eventType = null;
        _srcScript = null;
        _msgsIndex = 0;
        _msgsLength = 0;
        _skippable = false;

        _characterDisplayed = null;
        _spelling = false;

        transform.GetChild(0).gameObject.SetActive(false);
        finalTextObj.GetComponent<TextMeshProUGUI>().text = "";
    }
}