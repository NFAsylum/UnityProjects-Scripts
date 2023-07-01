using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctionsScript : MonoBehaviour
{
    [SerializeField]
    Button _firstButton;

    [SerializeField]
    GameObject pauseMenu;
    public GameObject transition;

    public bool _canPause = true, _saveScene;


    void Start()
    {
/*        if (SceneManager.GetActiveScene().name == "MainMenu Scene")
        {
            _firstButton.Select();
        }*/

        if (_saveScene)
        {
            try
            {
                PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
            }
            catch { }
        }
    }

    void Update()
    {
        PauseMenu();
    }

    public void ResetScene()
    {
        StartCoroutine(GameStart(SceneManager.GetActiveScene().name));
    }

    public void PanelSetActive(GameObject panel, bool value)
    {
        panel.SetActive(value);
    }

    public void StartScene(string scene)
    {
        StartCoroutine(GameStart(scene));
    }

    IEnumerator GameStart(string scene)
    {
        transition.SetActive(true);

        yield return new WaitForSeconds(Random.Range(1f, 3.2f));

        SceneManager.LoadScene(scene);
    }

    public void ExitApplication()
    {
        print("Exitting Application");
        Application.Quit();
    }

    public void PauseMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape) && _canPause)
        {
            //print("Pause");

            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
