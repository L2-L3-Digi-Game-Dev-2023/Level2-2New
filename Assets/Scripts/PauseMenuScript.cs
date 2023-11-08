using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SM = UnityEngine.SceneManagement.SceneManager;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    private static bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        SM.LoadScene(0);
    }

    public static void Pause(GameObject pause)
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPause = true;
    }

    public void ResumeButton()
    {
        Debug.Log("TESTT");
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
    }

    public static bool IsPaused
    {
        get => isPause;
        set => isPause = value;
    }
}
