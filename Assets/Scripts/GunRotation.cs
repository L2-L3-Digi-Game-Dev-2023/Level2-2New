using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
public class GunRotation : MonoBehaviour
{
    private static bool shooting = false;
    Vector3 def = new Vector3(-2.74f, 182.6f, 4.2f);
    Vector3 shoot = new Vector3(3.2f, 201.3f, -2.39f);

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = shooting ? Quaternion.Euler(shoot.x, shoot.y, shoot.z) : Quaternion.Euler(def.x, def.y, def.z);
    }

    public static bool Shooting{
        get {return shooting;}
        set {shooting = value;}
    }
}
}