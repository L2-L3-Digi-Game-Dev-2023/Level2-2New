using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
public class GunShootBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            GunRotation.Shooting = true;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            GunRotation.Shooting = false;
        }
        
    }
}
}