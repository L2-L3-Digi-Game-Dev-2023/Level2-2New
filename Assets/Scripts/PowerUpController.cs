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
        protected GameObject[] gObjs;
        public bool isCollisionScript;
        AmmoPU ammo;
        GameObject gameOb;
        DamagePU damage;
        HealthPU health;
        // Start is called before the first frame update
        void Start()
        {
            gObjs = GameObject.FindGameObjectsWithTag("Powerup");
            Debug.Log($"Powerups: {gObjs.Length}");
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
                foreach(GameObject obj in gObjs){
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
                        if(powerTexts[1].Count > 0) activate[0] = powerup.ActiveAction();
                    }
                    else if(powerup is AmmoPU){
                        if(powerTexts[0].Count > 0) activate[1] = powerup.ActiveAction();

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
                        if (gameOb != null && powerup != null && gameOb.name.ToLower().Contains(powerup.Name)) 
                        {    
                            powerup.Count++;
                            // **** Testing 
                            /*Debug.Log(gameOb.name.ToLower().Contains(powerup.Name).ToString() + " Condition checker");
                            Debug.Log(gameOb.name.ToLower() + " ColliderGOName");
                            Debug.Log(powerup.Name + " powerupName");
                            Debug.Log($"{powerup.Count,-5} powerupCount");
                            */
                            powerup.UpdateText();
                            powerup.SetColor();
                            Destroy(powerup.associatedObject);
                            powerups.Remove(powerup);
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