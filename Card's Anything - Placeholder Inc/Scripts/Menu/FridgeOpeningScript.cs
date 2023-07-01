using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeOpeningScript : MonoBehaviour
{
    bool _open;

    [SerializeField]
    Transform _initialPosition, _finalPosition;

    Animator _anim;



    void Start()
    {
        _initialPosition = transform;

        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        ChangeAnimation();
    }

    void OnMouseDown()
    {
        print("Clicked on " + gameObject.name);

        _open = !_open;
    }

    public void ChangeAnimation()
    {
        if (!_open)
        {
            _anim.SetBool("IsOpen", false);
        }
        else
        {
            _anim.SetBool("IsOpen", true);
        }
    }
}
