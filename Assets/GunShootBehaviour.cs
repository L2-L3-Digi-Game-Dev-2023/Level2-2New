/*
 * Name: Hayden Gillanders
 * Date Modified: 9 October 2023
 * File Purpose: Control the behaviour of the gun
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
public class GunShootBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask hittableLayer;
    [SerializeField] float weaponRange;
    [SerializeField] ParticleSystem muzzleFlash;
    Camera mainCam;
    
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
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            GunRotation.Shooting = true;
            HandleRaycast();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            GunRotation.Shooting = false;
        }
        
    }

    private void HandleRaycast(){
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, weaponRange, hittableLayer))
        {
            Debug.Log("Hit a wall");
        }
        else
        {
            Debug.Log("Not hit Hall");
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