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
        List<Bar> bars;
        // Start is called before the first frame update
        void Start()
        {
            bars = new List<Bar>();
            healthBar = new HealthBar(100,_sH,_tH);
            bars.Add(healthBar);
            ammoBar = new AmmoBar(150,_sA, _tA);
            bars.Add(ammoBar);
            foreach (Bar bar in bars) bar.RefreshBarText();
        }

        // Update is called once per frame
        void Update()
        {
            foreach(Bar bar in bars){
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
                healthBar.RefreshBarText();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                ammoBar.Decrease(10);
                ammoBar.RefreshBarText();
            }
            
        }
    }
}
