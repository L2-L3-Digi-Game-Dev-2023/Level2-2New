/*
 * PURPOSE: Check whether current has collided with bullet object, and adjust health as appropriate
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace
{
    public class BulletChecker : MonoBehaviour
    {
        //Health and ammo objects
        HealthBar health;
        AmmoBar ammo;
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            //Set health and ammo as appropriate Bars from BarsFunction script list
            health = (HealthBar)(BarsFunction.bars[0]);
            ammo = (AmmoBar)(BarsFunction.bars[1]);
        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// When this object collides with something
        /// </summary>
        /// <param name="other"></param>
        void OnCollisionEnter(Collision other)
        {
            //If the object has bullet in it's name 
            if (other.gameObject.name.ToLower().Contains("bullet"))
            {
                //if the bullet has bullet2, decrease by 20, else decrease by 5
                health.Decrease(other.gameObject.name.ToLower().Contains("bullet2") ? 20 : 5);
                //Destroy bullet
                Destroy(other.gameObject);
            }
        }
    }
}