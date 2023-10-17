
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Enemy enemyObj = new Enemy();
    // Start is called before the first frame update
    void Start()
    {
        enemyObj.AssocGO = this.gameObject;
        foreach(Enemy enemy in Enemies.EnemiesList.ToList()){
            if (enemy.AssocGO == this.gameObject){
                UnityEngine.Debug.Log("Synced");
                enemyObj = Enemies.EnemiesList[Enemies.EnemiesList.IndexOf(enemy)];
            }
        }
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
        timer.Stop();
        if(timer.Elapsed.Seconds >= 10){ //10 in prod
            degree = random.Next(0,360);
            timer.Restart();
        }
        else{
            timer.Start();
        }
        if(enemyObj.Moving){
        scalingFactor = 1; // Bigger for slower
        enemyObj.AssocGO.transform.localRotation = Quaternion.Slerp(enemyObj.AssocGO.transform.localRotation, Quaternion.Euler(0, degree, 0), Time.deltaTime/scalingFactor);

        enemyObj.AssocGO.transform.localPosition += enemyObj.AssocGO.transform.localRotation * new Vector3(0, 0, 2f * Time.deltaTime * SCALE_FACTOR);
        
        
        //transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        }else{

        enemyObj.AssocGO.transform.localPosition += new Vector3(0,0,0);
        
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
}
}