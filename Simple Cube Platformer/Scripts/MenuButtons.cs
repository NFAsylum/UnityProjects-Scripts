using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void ToQuit()
    {
        Application.Quit();
    }

    public void ToStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ToAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }

    public void ToSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ToAdventure()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void ToHorde()
    {
        SceneManager.LoadScene("HordeModeScene");
    }
}
