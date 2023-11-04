/*
 * Name: Hayden Gillanders
 * Date Created: 15 August 2023
 * Date Modified: 18 August 2023 (exc Header Comment)
 * File Purpose: Control Behaviour of UI Bars
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameNameSpace
{
    public class BarsFunction : MonoBehaviour
    {
        public Slider _sH,_sA;
        public TMP_Text _tH,_tA;
        HealthBar healthBar;
        AmmoBar ammoBar;
        public static List<Bar> bars;
        // Start is called before the first frame update
        void Awake()
        {
            bars = new List<Bar>();
            healthBar = new HealthBar(100,_sH,_tH);
            bars.Add(healthBar);
            ammoBar = new AmmoBar(150,_sA, _tA);
            bars.Add(ammoBar);
            

        }
        void Start()
        {
            // DO NOT REMOVE OTHERWISE UNITY CRASHES **************************************
            foreach (Bar bar in bars) bar.RefreshBarText(); // DO NOT REMOVE OTHERWISE UNITY CRASHES
            // DO NOT REMOVE OTHERWISE UNITY CRASHES **************************************
        }

        // Update is called once per frame
        void Update()
        {
            foreach(Bar bar in bars){
                if(bar is HealthBar barH && PowerUpController.ActivatedPowerups[0]){
                    barH.Increase(10);
                    PowerUpController.ActivatedPowerups[0] = false;
                }
                else if (bar is AmmoBar barA && PowerUpController.ActivatedPowerups[1]){
                    PowerUpController.ActivatedPowerups[1] = false;
                    
                    
                    
                    barA.Increase(10);

                }
                if(bar.BarZero()[0] == true){
                    switch(bar.BarZero()[1]){
                        case true:
                            bar.BarText = "NO HEALTH REMAINING";
                            break;
                        case false:
                            bar.BarText = "NO AMMO REMAINING";
                            break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)){
                healthBar.Decrease(10);
                Debug.Log(healthBar.CValue.ToString());
                
            }
            
            if(Input.GetKeyDown(KeyCode.R)){
                ammoBar.Decrease(10);
                
            }
            
        }
    }
}
