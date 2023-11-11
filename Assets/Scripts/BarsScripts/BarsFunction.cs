/*
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
        //The two UI sliders
        public Slider _sH,_sA;
        //The two UI slider texts
        public TMP_Text _tH,_tA;
        //HealthBar and ammobar objects objects
        static HealthBar healthBar;
        static AmmoBar ammoBar;
        //List of bars on the UI
        public static List<Bar> bars;
        /// <summary>
        /// Called when enabled for the first time
        /// </summary>
        void Awake()
        {
            //Create new bar list
            bars = new List<Bar>();
            //Add healthbar to the list
            healthBar = new HealthBar(100,_sH,_tH);
            bars.Add(healthBar);
            //Add ammobar to the list
            ammoBar = new AmmoBar(50,_sA, _tA);
            bars.Add(ammoBar);
            

        }
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            // DO NOT REMOVE OTHERWISE UNITY CRASHES **************************************
            //Refreshes the bar text upon start
            foreach (Bar bar in bars) bar.RefreshBarText(); // DO NOT REMOVE OTHERWISE UNITY CRASHES
            // DO NOT REMOVE OTHERWISE UNITY CRASHES **************************************
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            //for each bar in the list of bars
            foreach(Bar bar in bars){
                //If the bar is healthbar and there is an activated health powerup
                if(bar is HealthBar barH && PowerUpController.ActivatedPowerups[0]){
                    //Increase the healthbar by 10
                    barH.Increase(10);
                    //Reset the bool to false
                    PowerUpController.ActivatedPowerups[0] = false;
                }
                //Else if the bar is ammobar and there is an activated ammo powerup
                else if (bar is AmmoBar barA && PowerUpController.ActivatedPowerups[1]){
                    //Reset the bool to false
                    PowerUpController.ActivatedPowerups[1] = false;
                    //Increase ammo by 10
                    barA.Increase(10);

                }
                //If bars are zero
                if(bar.BarZero()[0] == true){
                    //Set appropriate bar to 0 text
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
            
            //If the game is playing and the user clicks, decrease ammo by 1
            if(Input.GetKeyDown(KeyCode.Mouse0) && TimeController.IsPlaying){
                ammoBar.Decrease(1);
                
            }
            
        }
        /// <summary>
        /// HealthBar property for use in other methods
        /// READONLY
        /// </summary>
        public static HealthBar HealthBarVar
        {
            get => healthBar;
        }
        /// <summary>
        /// AmmoBar property for use in other methods
        /// READONLY
        /// </summary>
        public static AmmoBar AmmoBarVar
        {
            get => ammoBar;
        }
    }

    
}
