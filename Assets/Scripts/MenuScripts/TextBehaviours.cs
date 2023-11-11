/*
 * Purpose: Set the colour of the text to an appropriate color
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBehaviours : MonoBehaviour
{
    //Text component
    public TMP_Text textComp;
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //set text colour to white
        textComp.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
