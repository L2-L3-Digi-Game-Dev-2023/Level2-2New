using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private static bool gameController;
    void Awake()
    {
        gameController = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void TogglePause(bool toggle)

    {



        if (toggle)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameController = false;
        }
        else if (!toggle)
        {
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameController = true;
        }
    }
    public static bool IsPlaying {
        get { return gameController; }
        set { gameController = value; }
    }
}
