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
        /// <summary>
        /// If colliding with player, set variables to collided object
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.name.ToLower().Contains("first person")) {collGO = this.gameObject;
            lCollision = other;
            }
        }
        /// <summary>
        /// If exit collision, reset variables
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionExit(Collision other)
        {
            lCollision = null;
            collGO = null;
        }
        /// <summary>
        /// Static collision var (get)
        /// </summary>
        public static Collision localCollision{
            get {return lCollision;}
        }
        /// <summary>
        /// Static collision GO var (get)
        /// </summary>
        public static GameObject CollidedGO{
            get {return collGO;}
        }
    }
}