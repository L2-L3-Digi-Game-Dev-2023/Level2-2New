/*
 * Global time controller for all movement and time-bound instructions
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private static bool gameController;
    /// <summary>
    /// Called when the script is first enabled
    /// </summary>
    void Awake()
    {
        //set game control to true
        gameController = true;
    }
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //Set time to normal
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Toggles the pause status
    /// </summary>
    /// <param name="toggle">What to set the status to</param>
    public static void TogglePause(bool toggle)
    { 
        //If game status has paused
        if (toggle)
        {
            //Freeze time
            Time.timeScale = 0f;
            //Make it possible for cursor to move
            Cursor.lockState = CursorLockMode.None;
            //Make cursor visible
            Cursor.visible = true;
            //Set gameplaying to false
            gameController = false;
        }
        //If game status has unpaused
        else if (!toggle)
        {
            //Unfreeze time
            Time.timeScale = 1.0f;
            //Lock cursor movement
            Cursor.lockState = CursorLockMode.Locked;
            //Hide cursor
            Cursor.visible = false;
            //Set game playing to true
            gameController = true;
        }
    }
    /// <summary>
    /// Public static property to show whether the game is playing (read and write)
    /// </summary>
    public static bool IsPlaying {
        get { return gameController; }
        set { gameController = value; }
    }
}
