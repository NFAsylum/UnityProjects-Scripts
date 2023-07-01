using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButtonScript : MonoBehaviour
{
    bool computerIsOn;
    [SerializeField]
    GameObject blackScreen, initializingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!computerIsOn)
        {
            computerIsOn = true;
            blackScreen.SetActive(false);
            initializingScreen.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(initializingScreen, 7.5f);
        }
    }
}
