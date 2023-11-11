/*
 * Rotate gun when necessary
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{
    public class GunRotation : MonoBehaviour
    {
        //Set shooting to false by default
        private static bool shooting = false;
        //Set vector3s of appropriate rotations
        Vector3 def = new Vector3(-2.74f, 182.6f, 4.2f);
        Vector3 shoot = new Vector3(3.2f, 201.3f, -2.39f);

    

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //Set rotations to different values based on whether shooting val is true 
            transform.localRotation = shooting ? Quaternion.Euler(shoot.x, shoot.y, shoot.z) : Quaternion.Euler(def.x, def.y, def.z);
        }
        /// <summary>
        /// get set shooting bool
        /// </summary>
        public static bool Shooting{
            get {return shooting;}
            set {shooting = value;}
        }
    }
}