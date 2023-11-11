/*
 * Script to control the behaviour of the credits panel
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    //GameObjects of main panel and credits panel
    public GameObject def;
    public GameObject creds;
    //Bool to store whether credits should be active
    static bool isCreds = false;
    void Awake()
    {
        //Set main to active and credits to inactive upon wake  
        def.SetActive(true);
        creds.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If should be credits, set credits to active and main to inactive else do opposite
        if (isCreds)
        {
            def.SetActive(false);
            creds.SetActive(true);
        }
        else if (!isCreds)
        {
            def.SetActive(true);
            creds.SetActive(false);
        }
    }
    /// <summary>
    /// Toggle creds bool
    /// </summary>
    public static void Toggle()
    {
        isCreds = !isCreds;
    }
}
