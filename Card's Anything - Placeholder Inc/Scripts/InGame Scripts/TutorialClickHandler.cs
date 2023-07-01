using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TutorialEventsScript _script;

    [SerializeField]
    KeyCode mouse0 = KeyCode.Mouse0;
    [SerializeField]
    KeyCode mouse1 = KeyCode.Mouse1;
    [SerializeField]
    KeyCode w = KeyCode.W;
    [SerializeField]
    KeyCode s = KeyCode.S;
    [SerializeField]
    KeyCode u = KeyCode.U;

    [SerializeField]
    Transform[] positions;
    [SerializeField]
    KeyCode[] inputs;

    bool _hovered;
    int _index = 0;

    void Start()
    {
        _script = GameObject.FindWithTag("Tutorial Manager").GetComponent<TutorialEventsScript>();

        transform.position = positions[_index].position;
        transform.localScale = positions[_index].localScale;

        _index++;
    }

    void Update()
    {
        if (_hovered && Input.GetKey(inputs[_index]))
        {
            ClickedOnHandler();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ClickedOnHandler();
        }
    }

    public void ClickedOnHandler()
    {
        print("Changed click handler");

        transform.position = positions[_index].position;
        transform.localScale = positions[_index].localScale;
        _script.ChangePanel();

        _index++;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Hovered");

        _hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
    }
}
