/*
 * PURPOSE - to control the movement of the minimap camera
 */

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //player's object
    public GameObject player;
    //Offset of the camera
    Vector3 offset = new Vector3(0, 100, 0);
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Place the camera above the player
        transform.position = player.transform.position + offset;
    }
}
