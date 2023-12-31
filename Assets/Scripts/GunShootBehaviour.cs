/*
 * Name: Hayden Gillanders
 * Date Modified: 9 October 2023
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



    Enemy GetEnemyInList(GameObject obj)
        {
            foreach (Enemy e in Enemies.EnemiesList) if (e.AssocGO == obj) return e;
            return null;
        }
    
    private void HandleRaycast(){
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, weaponRange, hittableLayer))
        {
            Debug.Log("Hit a wall");
            GameObject go = hit.transform.parent.gameObject;
                Debug.Log(go.name);
                Enemy en = GetEnemyInList(go);
                Debug.Log(en + en.AssocGO.name);
            EnemySelector.FindEnemy(en, en.AssocGO.GetComponent<NavMeshAgent>());
                en.Moving = false;

        }
        else
        {
            Debug.Log("Not hit wall");
        }
        HandleMuzzleFlash();
    }
    private void HandleMuzzleFlash()
    {
    if (muzzleFlash.isPlaying)
        muzzleFlash.Stop();
    muzzleFlash.Play();
    }
}
}