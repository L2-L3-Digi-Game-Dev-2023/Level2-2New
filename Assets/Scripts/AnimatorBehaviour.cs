/*
    File Purpose: Store instructions for animation
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using UnityEngine.AI;

namespace GameNameSpace{
    public class AnimatorBehaviour : MonoBehaviour
    {
        //Current navmesh
        NavMeshAgent navmesh;
        //current animator
        Animator animateComp;
        //Current enemy obj
        Enemy enemy;
        //check whether animator has changed for this yet
        bool hasPassed = false;
        //Rigidbody current
        Rigidbody rb;
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            //If no navmesh, assign null, else get component navmesh
            navmesh = (GetComponent<NavMeshAgent>() == null) ? null : GetComponent<NavMeshAgent>();
            //Set enemy to a new enemy based on this object
            enemy = new Enemy(this.gameObject);
            //Add this enemy to list
            Enemies.EnemiesList.Add(enemy);
            //Set animator component to this objects animator
            animateComp = gameObject.GetComponent<Animator>();
            //Set rigidbody to this objects rigidbody
            rb = GetComponent<Rigidbody>();
            //Set Object's mesh collider
            SetMesh();
        }

        // Update is called once per frame
        void Update()
        {
            //if current animation state is death, and has not gone through this enemy already
            if ((animateComp.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)")) && !hasPassed)
            {
                //Decrease the y position 
                transform.position -= new Vector3(0f, 1f, 0f);
                //Set passed to true
                hasPassed = true;
            }
            //If the death animation is over
            if ((animateComp.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.2) && animateComp.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)")){
                //Set death to false
                animateComp.SetBool("isDie", false);
                //Remove enemy from list
                Enemies.EnemiesList.Remove(enemy);
                //Destroy this object
                Destroy(this.gameObject);

            }
        }    
        /// <summary>
        /// Set the mesh collider of this object    
        /// </summary>
        void SetMesh()
        {
            //For each child in this object, assign a mesh if has one, else assign no mesh
            foreach (Transform child in transform)
            {
                //If object is not hips (as hips doens't have a mesh
                if (!child.gameObject.name.Contains("Hips"))
                {
                    //Set mesh to the renderer's mesh
                    Mesh mesh = child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;

                    if (mesh != null && child != null)
                    {
                        MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
                        meshCollider.sharedMesh = null;
                        meshCollider.sharedMesh = mesh;
                        Rigidbody rbch = child.gameObject.AddComponent<Rigidbody>();
                        rbch.isKinematic = true;
                    }
                }
            }
        }
    }
}
