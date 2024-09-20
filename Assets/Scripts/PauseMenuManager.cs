using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject continueButton;
    private GameObject exitButton;
    private GameObject pauseButton;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        continueButton = GameObject.Find("ContinueButton");
        exitButton = GameObject.Find("ExitButton");
        pauseButton = GameObject.Find("PauseButton");
        pauseMenu.SetActive(false);

        continueButton.GetComponent<Button>().onClick.AddListener(onContinueButtonPress);
        exitButton.GetComponent<Button>().onClick.AddListener(onExitButtonPress);
        pauseButton.GetComponent<Button>().onClick.AddListener(onPauseButtonPress);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            changePauseState();
        }
    }

    private void changePauseState()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        pauseButton.SetActive(!pauseButton.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    private void onContinueButtonPress()
    {
        changePauseState();
    }

    private void onPauseButtonPress()
    {
        if (!isPaused)
        {
            changePauseState();
        }
    }

    public void onExitButtonPress()
    {
        Application.Quit();
    }
}
