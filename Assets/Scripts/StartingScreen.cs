/*
 * Instructions to render starting screen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    public GameObject startGO;
    static bool isActive = true;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        startGO.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    void LateUpdate()
    {
        //If not active set to false
        if (!isActive)
        {
            startGO.SetActive(false);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Called by button to turn off question
    /// </summary>
    public static void OKButton()
    {
        isActive = false;
    }
}
