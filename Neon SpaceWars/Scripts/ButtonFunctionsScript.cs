using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionsScript : MonoBehaviour
{
    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
