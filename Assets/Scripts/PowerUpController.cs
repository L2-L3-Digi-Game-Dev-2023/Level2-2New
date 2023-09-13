/*
 * Name: Hayden Gillanders
 * Date Created: 21 August 2023
 * Date Modified: 22 August 2023
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
            foreach (GameObject gob in GameObject.FindGameObjectsWithTag("Powerup")){
                gObjs.Add(gob);
            }
            Debug.Log($"Powerups: {gObjs.Count}");
            powerups = new List<Powerups>();

            if (!isCollisionScript){
                ammo = new AmmoPU(texts[0]);
                damage = new DamagePU(texts[1]);
                health = new HealthPU(texts[2]);

                powerTexts[0] = ammo;
                powerTexts[1] = damage;
                powerTexts[2] = health;
            }
            else if (isCollisionScript){
                foreach(GameObject obj in gObjs.ToList()){
                    if (obj.name.ToLower().Contains("ammo")){
                        powerups.Add(new AmmoPU(texts[0], obj));
                        Debug.Log("added ammo");
                    }
                    else if (obj.name.ToLower().Contains("heart")){
                        powerups.Add(new HealthPU(texts[2], obj));
                        Debug.Log("added heart");
                    }
                    else if (obj.name.ToLower().Contains("damage")){
                        powerups.Add(new DamagePU(texts[1], obj));
                        Debug.Log("Added damage");
                    }
                    else{
                        Debug.Log($"Incorrect assignment to gObjs List in PowerUpController of {obj.name} {obj}");
                    }
                    gObjs.Remove(obj);
                }
            }
            powerups.Add(ammo);
            powerups.Add(damage);
            powerups.Add(health);

            //Debug.Log($"Powerups: {gObjs.Length}");
            
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Powerups powerup in powerups.ToList()){
                if(powerup != null && powerup.IsText) {
                    powerup.SetColor();
                }
                //Debug.Log(powerup); 
            }
            if(!isCollisionScript){
            foreach(Powerups powerup in powerups.ToList()){
                if(powerup.associatedObject == null){
                if (Input.GetKeyDown(powerup.Key)){
                    if(powerup is HealthPU){
                        activate[0] = powerup.CanInc?powerup.ActiveAction() : false;
                    }
                    else if(powerup is AmmoPU){
                        activate[1] = powerup.CanInc?powerup.ActiveAction():false;
                    }
                    else if (powerup is DamagePU){
                        //Add functionality for damage

                        Debug.Log("Damageusage registered");
                        if(powerTexts[1].Count > 0) {Debug.Log("Damageusage success");}

                    }
                    
                }
                
                
                
                
                //For testing **********************
                /*if (Input.GetKeyDown(powerup.Key)){
                    powerup.Count++;
                    powerup.UpdateText();
                }*/
                //END for testing ******************
                
                
                
                
                powerup.SetColor();
                }
            }
            }
            else{
                //Debug.Log("TEST if access");
                if(LocalCollision.localCollision != null){
                    gameOb = LocalCollision.CollidedGO;
                    //Debug.Log(gameOb);
                    //Debug.Log("IEFJOSIJEFIOJ");
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
                            // **** Testing 
                            /*Debug.Log(gameOb.name.ToLower().Contains(powerup.Name).ToString() + " Condition checker");
                            Debug.Log(gameOb.name.ToLower() + " ColliderGOName");
                            Debug.Log(powerup.Name + " powerupName");
                            Debug.Log($"{powerup.Count,-5} powerupCount");
                            */
                            powerup.UpdateText();
                            powerup.SetColor();
                            Destroy(powerup.associatedObject);
                        }
                    }
                }
            }

        }

        public static bool[] ActivatedPowerups{
            get {return activate;}
            set {activate = value;}
        }

        public static bool ActiveDamage{
            get{return activeDamage;}
            set{activeDamage = value;}
        }

    }
}