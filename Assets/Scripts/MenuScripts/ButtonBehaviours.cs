/*
 * Behaviour of Main Menu buttons
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviours : MonoBehaviour
{
    /// <summary>
    /// Quit the application
    /// </summary>
    public void LoadQuit(){
        Application.Quit();
    }

    /// <summary>
    /// Load the first scene
    /// </summary>
    public void LoadStart(){
        SceneManager.LoadScene(1);
    }
}
