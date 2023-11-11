/*
 * Behaviour of panels
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Whenever the panel is enabled pause
    /// </summary>
    void OnEnable()
    {
        TimeController.TogglePause(true);
    }
    /// <summary>
    /// Whenever panel is disabled unpause
    /// </summary>
    void OnDisable()
    {
        Debug.Log("DISABLED");
        TimeController.TogglePause(false);
    }
}
