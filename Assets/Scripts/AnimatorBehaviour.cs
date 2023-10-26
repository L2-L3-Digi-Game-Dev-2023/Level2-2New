/*
    Name: Hayden Gillanders
    Date Modified: 9 October 2023
    File Purpose: Store instructions for animation
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Diagnostics;

namespace GameNameSpace{
public class AnimatorBehaviour : MonoBehaviour
{
    Animator animateComp;
    Enemy enemy;
    System.Random random;
    const float SCALE_FACTOR = 0.5f;
    Stopwatch timer;
    int scalingFactor;
    int degree;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = new Enemy(this.gameObject);
        Enemies.EnemiesList.Add(enemy);
        animateComp = gameObject.GetComponent<Animator>();
        random = new System.Random();
        degree = random.Next(0,360);
        timer = new Stopwatch();
        timer.Start();
        rb = GetComponent<Rigidbody>();
        foreach(Transform child in transform){
            if (!child.gameObject.name.Contains("Hips")){
            Mesh mesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            if (mesh != null && child != null){
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
        if ((animateComp.GetCurrentAnimatorStateInfo(0).length <=
           animateComp.GetCurrentAnimatorStateInfo(0).normalizedTime) && animateComp.GetCurrentAnimatorStateInfo(0).IsName("Death From Right  (1)")){
            Destroy(this.gameObject);
           }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            animateComp.SetBool("isDie", true);
            enemy.Moving = false;
        }
        timer.Stop();
        if(timer.Elapsed.Seconds >= 10){ //10 in prod
            degree = random.Next(0,360);
            timer.Restart();
        }
        else{
            timer.Start();
        }
        if(enemy.Moving){
        scalingFactor = 1; // Bigger for slower
        enemy.AssocGO.transform.localRotation = Quaternion.Slerp(enemy.AssocGO.transform.localRotation, Quaternion.Euler(0, degree, 0), Time.deltaTime/scalingFactor);

        enemy.AssocGO.transform.localPosition += enemy.AssocGO.transform.localRotation * new Vector3(0, 0, 2f * Time.deltaTime * SCALE_FACTOR);
        
        
        //transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        }

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
    
    bool AnimatorIsPlaying(){
    return animateComp.GetCurrentAnimatorStateInfo(0).length >
           animateComp.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }  
    bool AnimatorIsPlaying(string stateName){
    return AnimatorIsPlaying() && animateComp.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
}
