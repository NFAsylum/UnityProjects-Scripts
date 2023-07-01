using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableOnTimerScript : MonoBehaviour
{
    [SerializeField]
    bool disable, enable;

    [SerializeField]
    GameObject[] objectsToDisable, objectsToEnable;

    [SerializeField]
    float[] disableTimer, enableTimer;


    // Start is called before the first frame update
    void Start()
    {
        if (disable)
        {
            for (int i = 0; i < objectsToDisable.Length; i++)
            {
                StartCoroutine(Action(objectsToDisable[i], "disable", disableTimer[i]));
            }
        }
        if (enable)
        {
            for (int i = 0; i < objectsToEnable.Length; i++)
            {
                StartCoroutine(Action(objectsToEnable[i], "enable", enableTimer[i]));
            }
        }
    }

    IEnumerator Action(GameObject obj, string mode, float timer)
    {
        yield return new WaitForSeconds(timer);

        if (mode == "disable")
        {
            obj.SetActive(false);
        }
        else if (mode == "enable")
        {
            obj.SetActive(true);
        }
    }
}
