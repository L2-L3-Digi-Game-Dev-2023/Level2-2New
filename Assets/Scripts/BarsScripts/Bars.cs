/*
 * File Purpose: Store Super- and Sub-Class information of UI Bars
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameNameSpace
{
    public class Bar
    {
        //Slider of the bar
        protected Slider slider;
        //Text of the bar
        protected TMP_Text barText;
        /// <summary>
        /// Initialize a bar object
        /// </summary>
        /// <param name="maxVal">maximum value of a bar</param>
        /// <param name="s">Slider of the bar</param>
        /// <param name="t">text of the bar</param>
        public Bar(int maxVal, Slider s, TMP_Text t){
            //Set this slider to param
            this.slider = s;
            //set slider maxvalue to param
            this.slider.maxValue = maxVal;
            //Set current value to maxvalue
            this.CValue = this.MValue;
            //Set bartext to param
            this.barText = t;
        }
        /// <summary>
        /// Get and set text of the bar
        /// </summary>
        public string BarText{
            get{return barText.text;}
            set{barText.text = value;}
        }
        /// <summary>
        /// Get and Set current bar value
        /// </summary>
        public int CValue{
            get {return (int)slider.value;}
            //If provided value greater than max, set to max else set to provided
            set {
                slider.value = (value <= slider.maxValue) ? value : MValue;
            }
        }
        /// <summary>
        /// Get max bar value
        /// </summary>
        public int MValue{
            get {return (int)slider.maxValue;}
        }
        /// <summary>
        /// Decrease the bar value
        /// </summary>
        /// <param name="amount">Amount to decrease by</param>
        public void Decrease(int amount){
            //If the value would be less than 0, set to 0 else take provided amount away
            CValue = ((CValue - amount) >= 0) ? CValue - amount : 0;
            //Refresh the bartext
            RefreshBarText();
        }
        /// <summary>
        /// Increase the bar value
        /// </summary>
        /// <param name="amount">Amount to increase by</param>
        public void Increase(int amount){
            //If the value would be greater than max, set to max, else increase by provided amount
            CValue = ((CValue + amount) <= MValue) ? CValue + amount : MValue;
            //Refresh the bar text
            RefreshBarText();
        }
        /// <summary>
        /// Refresh the bar text to show appropriate values
        /// </summary>
        public void RefreshBarText(){
            this.BarText = $"{this.CValue}/{this.MValue}";
        }
        /// <summary>
        /// Return bool array identifying whether this bar is 0
        /// </summary>
        /// <returns></returns>
        public virtual bool[] BarZero(){
            //initialize bool array
            bool[] returnArr = new bool[2];
            //Set first element to (cvalue <= 0) 
            returnArr[0] = this.CValue <= 0;
            //Return the bool array
            return returnArr;
        }
    }
    //Ammo bar inheriting from Bar
    public class AmmoBar:Bar{
        /// <summary>
        /// Constructor with same params as super, no extra instructions
        /// </summary>
        /// <param name="maxVal"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public AmmoBar(int maxVal, Slider s, TMP_Text t) : base(maxVal, s, t){}

        public override bool[] BarZero(){
            bool[] returnArr = base.BarZero();
            //Set second element to false (Second element tells whether or not is health bar
            returnArr[1] = false;
            return returnArr;
        }

    }
    //Health bar inheriting from Bar
    public class HealthBar:Bar{
        /// <summary>
        /// Constructor with same params as super, no extra instructions
        /// </summary>
        /// <param name="maxVal"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public HealthBar(int maxVal, Slider s, TMP_Text t) : base(maxVal, s, t){}

        public override bool[] BarZero(){
            bool[] returnArr = base.BarZero();
            //Set second element to true (Second element tells whether or not is health bar)
            returnArr[1] = true;
            return returnArr;
        }
    }
}