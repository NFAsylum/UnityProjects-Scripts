using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventsScript : MonoBehaviour
{
    [SerializeField]
    bool[] _needsApproval;

    int index = -1;

    [SerializeField]
    GameObject[] panels;

    [SerializeField]
    bool _anyKeyAnyPlaceTutorial;


    void Update()
    {
        if (_anyKeyAnyPlaceTutorial)
        {
            if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.LeftWindows)))
            {
                ChangePanel();
            }
        }
    }

    public void ChangePanel()
    {
        //print("Change panel");
        
        if (!_needsApproval[index++])
        {
            if (index < panels.Length - 1)
            {
                index++;

                panels[index].SetActive(true);

                if (index > 0)
                {
                    panels[index - 1].SetActive(false);
                }
            }
            else
            {
                panels[index - 1].SetActive(false);

                print("Tutorial Finished");

                gameObject.SetActive(false);
            }
        }
        else
        {

        }
    }
}
