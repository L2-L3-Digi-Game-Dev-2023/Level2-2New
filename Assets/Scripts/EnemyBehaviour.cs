
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
namespace GameNameSpace{
public class EnemyBehaviour : MonoBehaviour
{
    System.Random random;
    const float SCALE_FACTOR = 0.5f;
    Stopwatch timer;
    int scalingFactor;
    int degree;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
        random = new System.Random();
        degree = random.Next(0,360);
        timer = new Stopwatch();
        timer.Start();
        rb = GetComponent<Rigidbody>();
        foreach(Transform child in transform){
            if (!child.gameObject.name.Contains("Hips")){
            Mesh mesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            if (mesh != null){
                UnityEngine.Debug.Log("test");
                MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;
                Rigidbody rbch = child.gameObject.AddComponent<Rigidbody>();
                rbch.isKinematic = true;
            }
            }
        }
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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, degree, 0), Time.deltaTime/scalingFactor);

        transform.localPosition += transform.localRotation * new Vector3(0, 0, 2f * Time.deltaTime * SCALE_FACTOR);
        //transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
    void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.name.Contains("player")){
            degree *= -1;
            if (timer.IsRunning) timer.Restart();
            UnityEngine.Debug.Log("aeorgnaejitnhoarthn");
            Update();
        }

    }
}
}