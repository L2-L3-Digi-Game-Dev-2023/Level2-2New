/*
    Name: Hayden Gillanders
    Date Modified: 9 October 2023
    File Purpose: Store instructions for animation
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{
    Animator animateComp;
    // Start is called before the first frame update
    void Start()
    {
        animateComp = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
