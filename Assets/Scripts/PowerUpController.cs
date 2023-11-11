/*
 * File Purpose: Control Behaviour of Powerups
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
namespace GameNameSpace
{
    public class PowerUpController : MonoBehaviour
    {
        //Variables
        private static bool[] activate = {false,false};
        private static bool activeDamage = false;
        public List<Powerups> powerups;
        private Powerups[] powerTexts = new Powerups[3];
        public TMP_Text[] texts = new TMP_Text[3];
        protected List<GameObject> gObjs = new List<GameObject>();
        public bool isCollisionScript;
        
        
        AmmoPU ammo;
        GameObject gameOb;
        DamagePU damage;
        HealthPU health;
        // Start is called before the first frame update
        void Start()
        {
            //for every gameobject that has powerup tag
            foreach (GameObject gob in GameObject.FindGameObjectsWithTag("Powerup")){
                //Add gameobject to list
                gObjs.Add(gob);
            }
            //Initialize powerup list
            powerups = new List<Powerups>();
            //If this isn't a colliding script, set powerup vars and powerup texts
            if (!isCollisionScript){
                ammo = new AmmoPU(texts[0]);
                damage = new DamagePU(texts[1]);
                health = new HealthPU(texts[2]);

                powerTexts[0] = ammo;
                powerTexts[1] = damage;
                powerTexts[2] = health;
            }
            //If is collision scripts, add to list with appropriate parameters
            else if (isCollisionScript){

                foreach(GameObject obj in gObjs.ToList()){

                    if (obj.name.ToLower().Contains("ammo")){

                        powerups.Add(new AmmoPU(texts[0], obj));
                    
                    }
                    else if (obj.name.ToLower().Contains("heart")){

                        powerups.Add(new HealthPU(texts[2], obj));

                    }
                    else if (obj.name.ToLower().Contains("damage")){

                        powerups.Add(new DamagePU(texts[1], obj));

                    }
                    gObjs.Remove(obj);
                }
            }
            //Add powerups to list
            powerups.Add(ammo);
            powerups.Add(damage);
            powerups.Add(health);

            //Debug.Log($"Powerups: {gObjs.Length}");
            
        }

        // Update is called once per frame
        void Update()
        {
            //For each powerup in powerup list, if is text set color
            foreach (Powerups powerup in powerups.ToList()){
                if(powerup != null && powerup.IsText) {
                    powerup.SetColor();
                }
            }
            //If isn't collision script
            if(!isCollisionScript){

                //foreach powerup in list, if their key is pressed, if can increment activate action
                foreach(Powerups powerup in powerups.ToList()){
                    if(powerup.associatedObject == null){
                    if (Input.GetKeyDown(powerup.Key)){
                        if(powerup is HealthPU){
                            activate[0] = powerup.CanInc?powerup.ActiveAction() : false;
                        }
                        else if(powerup is AmmoPU){
                            activate[1] = powerup.CanInc?powerup.ActiveAction():false;
                        }                    
                    }              
                    //Set powerup color
                    powerup.SetColor();
                    }
                }
            }
            //if not collision script
            else{
                //if a collision has happened
                if(LocalCollision.localCollision != null){
                    gameOb = LocalCollision.CollidedGO;
                    //Add appropriate powerup, update text, set colour, destroy poweurp object
                    foreach(Powerups powerup in powerups.ToList()){
                        if (gameOb != null && powerup != null && powerup.associatedObject == gameOb && gameOb.name.ToLower().Contains(powerup.Name)) 
                        {    
                            if(powerup is HealthPU he){
                                HealthPU.AddHealth(1);
                            }
                            else if(powerup is AmmoPU am){
                                AmmoPU.AddAmmo(1);
                            }
                            else if(powerup is DamagePU dm){
                                DamagePU.AddDamage(1);
                            }
                            powerup.UpdateText();
                            powerup.SetColor();
                            Destroy(powerup.associatedObject);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// Get and set activated powerups
        /// </summary>
        public static bool[] ActivatedPowerups{
            get {return activate;}
            set {activate = value;}
        }
        /// <summary>
        /// Get and set active damage
        /// </summary>
        public static bool ActiveDamage{
            get{return activeDamage;}
            set{activeDamage = value;}
        }

    }
}