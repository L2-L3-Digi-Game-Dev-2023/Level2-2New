using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
public class LocalCollision : MonoBehaviour
{
    private static Collision lCollision;
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
        lCollision = other;
    }

    void OnCollisionExit(Collision other)
    {
        lCollision = null;
    }
    public static Collision localCollision{
        get {return lCollision;}
    }
}
}