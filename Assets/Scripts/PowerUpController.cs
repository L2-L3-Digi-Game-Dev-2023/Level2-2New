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
        protected List<Powerups> powerups;
        public TMP_Text[] texts = new TMP_Text[3];
        protected GameObject[] gObjs;
        public bool isCollisionScript;
        AmmoPU ammo;
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
            }
            else if (isCollisionScript){
                foreach(GameObject obj in gObjs){
                    if (obj.name.ToLower().Contains("ammo")){
                        powerups.Add(new AmmoPU(obj));
                        Debug.Log("added ammo");
                    }
                    else if (obj.name.ToLower().Contains("heart")){
                        powerups.Add(new HealthPU(obj));
                        Debug.Log("added heart");
                    }
                    else if (obj.name.ToLower().Contains("damage")){
                        powerups.Add(new DamagePU(obj));
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

            Debug.Log($"Powerups: {gObjs.Length}");

        }

        // Update is called once per frame
        void Update()
        {
            if(!isCollisionScript){
            foreach(Powerups powerup in powerups.ToList()){
                if(powerup.associatedObject == null){
                //For testing **********************
                if (Input.GetKeyDown(powerup.Key)){
                    powerup.Count++;
                    powerup.UpdateText();
                }
                //END for testing ******************
                powerup.SetColor(powerup.ColorCheck());
                }
            }
            }

        }

        
        
    }
}