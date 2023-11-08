using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScript : MonoBehaviour
{
    private GameObject pauseMenu;
    void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu");
        

        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuScript.Pause(pauseMenu);
        }
    }

    void OnEnable()
    {
        Debug.Log(Cursor.lockState);
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenuScript.IsPaused = false;
        pauseMenu.SetActive(false);
    }
}
