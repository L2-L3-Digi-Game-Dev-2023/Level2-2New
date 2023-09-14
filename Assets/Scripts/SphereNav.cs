/* **************** *
 * AUTHOR: Hayden G
 * FILE NAME: Behvaiours
 * FILE DESC: Directs behaviours of Spheres
 * **************** *
 * DATE CREATED: 26 July 2023
 * DATE UPDATED: 23 August 2023
 * UPDATE DETAILS: Lambda Func<T,TResult> added to correctly adjust sphere localScale based on isPowerup array (23 Aug)
 *               - Added new instructions for spheres to be correctly organised in UI Gameobject (23 Aug)
 *               - 
 *               -
 * **************** */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameNameSpace{
public class SphereNav : MonoBehaviour
{
    public bool isEnemy;
    public bool[] isPowerups = new bool[3];
    GameObject sphere;
    Vector3 offset;
    GameObject obj;
    Func<bool[], bool> checkForPowerups = bools => {
        foreach(bool val in bools){
            if (val) return true;
        }
        return false;
    };
    
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.Find("Minimap Spheres") == null) obj = new GameObject("Minimap Spheres");
        else obj = GameObject.Find("Minimap Spheres");
        obj.transform.SetParent(GameObject.Find("UI").transform);
        offset = (isEnemy||checkForPowerups(isPowerups)) ? new Vector3(0, 0, 0) : new Vector3(0, 2, 0);
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.sphere.transform.position = this.transform.position + offset;
        this.sphere.transform.localScale = checkForPowerups(isPowerups) ? new Vector3(3,3,3) : new Vector3(4, 4, 4);
        this.sphere.layer = LayerMask.NameToLayer("Minimap");
        Destroy(this.sphere.GetComponent<Collider>());

    }

    // Update is called once per frame
    void Update()
    {
       
        var spherRender = (this.sphere.GetComponent<Renderer>()!=null) ? this.sphere.GetComponent<Renderer>() : null;
        if(spherRender != null){
        sphere.transform.SetParent(obj.transform);
        this.sphere.transform.position = this.transform.position+offset;
        if (isEnemy) spherRender.material.SetColor("_Color", new Color(255, 0, 0));
        else if (!(isEnemy)) spherRender.material.SetColor("_Color", new Color(0, 0, 255));
        if(checkForPowerups(isPowerups))
        {
            Debug.Log()
            if (isPowerups[0]) spherRender.material.SetColor("_Color", new Color(255,255,0));
            else if (isPowerups[1]) spherRender.material.SetColor("_Color", new Color(0.87f,0.19f,0.39f));
            else if (isPowerups[2]) spherRender.material.SetColor("_Color", new Color(0.13f,0.55f,0.13f));
        }
        }
        
    }

    void OnDisable()
    {
        Destroy(sphere);
    }
}
}