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
    void OnEnable()
    {
        Debug.Log("ENABLED");
        TimeController.TogglePause(true);
    }

    void OnDisable()
    {
        Debug.Log("DISABLED");
        TimeController.TogglePause(false);
    }
}
