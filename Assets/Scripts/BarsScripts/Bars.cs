/*
 * Name: Hayden Gillanders
 * Date Created: 15 August 2023
 * Date Modified: 18 August 2023 (exc comment Header)
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
        protected Slider slider;
        protected TMP_Text barText;
        public Bar(int maxVal, Slider s, TMP_Text t){
            this.slider = s;
            this.slider.maxValue = maxVal;
            this.CValue = this.MValue;
            this.barText = t;
        }

        public string BarText{
            get{return barText.text;}
            set{barText.text = value;}
        }
        public int CValue{
            get {return (int)slider.value;}
            set {
                slider.value = (value <= slider.maxValue) ? value : slider.value;
            }
        }

        public int MValue{
            get {return (int)slider.maxValue;}
        }

        public void Decrease(int amount){
            CValue = ((CValue - amount) >= 0) ? CValue - amount : 0;
            RefreshBarText();
        }

        public void Increase(int amount){
            CValue = ((CValue + amount) <= MValue) ? CValue + amount : MValue;
            RefreshBarText();
        }

        public void RefreshBarText(){
            this.BarText = $"{this.CValue}/{this.MValue}";
        }

        public virtual bool[] BarZero(){
            bool[] returnArr = new bool[2];
            returnArr[0] = this.CValue <= 0;
            return returnArr;
        }
    }
    public class AmmoBar:Bar{
        public AmmoBar(int maxVal, Slider s, TMP_Text t) : base(maxVal, s, t){}

        public override bool[] BarZero(){
            bool[] returnArr = base.BarZero();
            returnArr[1] = false;
            return returnArr;
        }

    }
    public class HealthBar:Bar{
        public HealthBar(int maxVal, Slider s, TMP_Text t) : base(maxVal, s, t){}

        public override bool[] BarZero(){
            bool[] returnArr = base.BarZero();
            returnArr[1] = true;
            return returnArr;
        }
    }
}