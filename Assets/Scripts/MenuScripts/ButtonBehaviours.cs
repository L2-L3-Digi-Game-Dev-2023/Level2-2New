    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviours : MonoBehaviour
{
    public void LoadQuit(){
        Application.Quit();
    }

    public void LoadStart(){
        SceneManager.LoadScene(1);
    }
}
