/*
 * File Purpose: Control the behaviour of the gun
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace GameNameSpace{
    public class GunShootBehaviour : MonoBehaviour
    {
        [SerializeField] LayerMask hittableLayer;
        [SerializeField] float weaponRange;
        [SerializeField] ParticleSystem muzzleFlash;
        Camera mainCam;
        Enemy thisEnemy;
        void Awake()
        {
            mainCam = Camera.main;
        }
    
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //If game is playing, handle gun shooting 
            if (TimeController.IsPlaying)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GunRotation.Shooting = true;
                    HandleRaycast();
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    GunRotation.Shooting = false;
                }
            }
        }


        /// <summary>
        /// Get enemy from the list
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Enemy GetEnemyInList(GameObject obj)
        {
            foreach (Enemy e in Enemies.EnemiesList) if (e.AssocGO == obj) return e;
            return null;
        }
    /// <summary>
    /// handle gun shoot raycast
    /// </summary>
        private void HandleRaycast(){
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, weaponRange, hittableLayer))
            {
                //Set gameobject to the parent of the hit
                GameObject go = hit.transform.parent.gameObject;
                //Find enemy in enemies list
                    Enemy en = GetEnemyInList(go);
                //Find enemy with enemy selector (updates animator)
                EnemySelector.FindEnemy(en, en.AssocGO.GetComponent<NavMeshAgent>());
                //Set moving to false    
                en.Moving = false;

            }
            //Muzzle flash
            HandleMuzzleFlash();
        }
        /// <summary>
        /// Handling muzzle flashing
        /// </summary>
        private void HandleMuzzleFlash()
        {
            //If is playing stop
        if (muzzleFlash.isPlaying)
            muzzleFlash.Stop();
        //Start
        muzzleFlash.Play();
        }
    }
}