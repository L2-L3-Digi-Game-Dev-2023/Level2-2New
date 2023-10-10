/*
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
public class LocalCollision : MonoBehaviour
{
    private static Collision lCollision;
    private static GameObject collGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"FROM LCOLL - GAMEOBJECT NAME IS {other.gameObject.name}\nTHIS NAME IS {this.gameObject.name}");
        if(other.gameObject.name.ToLower().Contains("player")) {collGO = this.gameObject;
        lCollision = other;
        }
    }

    void OnCollisionExit(Collision other)
    {
        lCollision = null;
        collGO = null;
    }
    public static Collision localCollision{
        get {return lCollision;}
    }
    public static GameObject CollidedGO{
        get {return collGO;}
    }
}
}