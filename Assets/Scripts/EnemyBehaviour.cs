
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
namespace GameNameSpace{
public class EnemyBehaviour : MonoBehaviour
{
    System.Random random;
    Stopwatch timer;
    int scalingFactor;
    int degree;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        degree = random.Next(0,360);
        timer = new Stopwatch();
        timer.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer.Stop();
        if(timer.Elapsed.Seconds >= 10){ //10 in prod
            degree = random.Next(0,360);
            timer.Restart();
        }
        else{
            timer.Start();
        }

        scalingFactor = 1; // Bigger for slower
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x, degree, transform.localRotation.z), Time.deltaTime/scalingFactor);

        this.GetComponent<Rigidbody>().velocity = Vector3.right * 10f;
    }
    void OnCollisionEnter(Collision other)
    {
        //Do stuff
    }
}
}