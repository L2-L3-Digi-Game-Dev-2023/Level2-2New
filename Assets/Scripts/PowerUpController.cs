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
        AmmoPU ammo;
        DamagePU damage;
        HealthPU health;
        // Start is called before the first frame update
        void Start()
        {
            gObjs = GameObject.FindGameObjectsWithTag("Powerup");
            
            Debug.Log($"Powerups: {gObjs.Length}");
            powerups = new List<Powerups>();
            ammo = new AmmoPU(texts[0]);
            damage = new DamagePU(texts[1]);
            health = new HealthPU(texts[2]);
            powerups.Add(ammo);
            powerups.Add(damage);
            powerups.Add(health);

            Debug.Log($"Powerups: {gObjs.Length}");

        }

        // Update is called once per frame
        void Update()
        {
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