/*
 * Name: Hayden Gillanders
 * Date Created: 18 August 2023
 * Date Modified: 22 August 2023
 * File Purpose: Store Super- and Sub-Class information of powerups
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace GameNameSpace{
    public abstract class Powerups
    {

        protected KeyCode PUKey;
        protected TMP_Text text;
        protected int count = 0;
        protected const int MAX_COUNT = 10;
        protected bool isMax;
        protected bool isText;
        protected Color maxColor, defColor, color;
        protected GameObject assocGameObject = null;

        /// <summary>
        /// Blank constructor initializes maxcolor and defcolor for texts
        /// </summary>
        public Powerups(){
            maxColor = new Color(1f,0.48f,0f);
            defColor = new Color(1f,1f,1f);
        }
        /// <summary>
        /// Text component constructor initializes text component, colors, and istext bool
        /// </summary>
        /// <param name="_t"></param>
        public Powerups(TMP_Text _t){
            text = _t;
            maxColor = new Color(1f,0.48f,0f);
            defColor = new Color(1f,1f,1f);
            isText = true;
        }
        /// <summary>
        /// Behaviour of a powerup when collided with
        /// </summary>
        public abstract void CollisionBehaviour();
        /// <summary>
        /// Execute action when activated
        /// </summary>
        /// <returns></returns>
        public abstract bool ActiveAction();
        /// <summary>
        /// Return whether can increment
        /// </summary>
        public abstract bool CanInc{get;}
        /// <summary>
        /// Updates the text
        /// </summary>
        public void UpdateText(){
            Text = Count.ToString();
        }
        /// <summary>
        /// Sets the colour based on whether or not the value is max
        /// </summary>
        public void SetColor(){
            text.color = isMax ? maxColor : defColor;
        }
        /// <summary>
        /// gets gameobject associated with the instance
        /// </summary>
        public GameObject associatedObject
        {
            get { return assocGameObject; }
        }
        /// <summary>
        /// gets whether or not the instance is a text instance
        /// </summary>
        public bool IsText
        {
            get
            {
                return isText;
            }
        }
        /// <summary>
        /// Gets the name of the instance
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// gets the key associated with the powerup
        /// </summary>
        public KeyCode Key
        {
            get { return PUKey; }
        }
        /// <summary>
        /// Gets the text from the text var of the instance
        /// </summary>
        public string Text
        {
            get { return text.text; }
            set { text.text = value; }
        }
        /// <summary>
        /// Gets and sets the count of the instance
        /// </summary>
        public abstract int Count
        {
            get;
            set;
        }

        }
    public class HealthPU : Powerups{
        int increase;
        static int healthNums = 0;
        /// <summary>
        /// Same as base but adds keycode
        /// </summary>
        /// <param name="_t"></param>
        public HealthPU(TMP_Text _t) : base(_t)
        {PUKey = KeyCode.Alpha2;}
        /// <summary>
        /// Same as base but adds keycode and increment
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_inc"></param>
        public HealthPU(TMP_Text _t, int _inc) : base(_t)
        {
            increase = _inc;
            PUKey = KeyCode.Alpha2;
        }
        /// <summary>
        /// Same as base but adds keycode and associated game object
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_object"></param>
        public HealthPU(TMP_Text _t, GameObject _object) : base(_t){
            assocGameObject = _object;

            isText = false;
        }
        public override string Name{get{return "heart";}}
        public override int Count{
            get {return Nums;}
            set {AddHealth(value);}
        }
        public override void CollisionBehaviour()
        {
            this.Count++;
        
        }
        public override bool ActiveAction(){
            healthNums--;
            UpdateText();
            return true;
        }
        /// <summary>
        /// Adds num to number of healths
        /// </summary>
        /// <param name="num"></param>
        public static void AddHealth(int num){
            healthNums+=num;
        }
        /// <summary>
        /// Get and set number of healths
        /// </summary>
        public static int Nums{
            get {return healthNums;}
            set{AddHealth(value);}
        }

        public override bool CanInc{
            get{return Count>0;}
        }
    }

    public class AmmoPU : Powerups{
        int increase;
        static int AmmoNums = 0;
        /// <summary>
        /// Same as base but adds keycode
        /// </summary>
        /// <param name="_t"></param>
        public AmmoPU(TMP_Text _t) : base(_t)
        {PUKey = KeyCode.Alpha3;}
        /// <summary>
        /// Same as base but adds keycode and increment
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_inc"></param>
        public AmmoPU(TMP_Text _t, int _inc) : base(_t)
        {
            increase = _inc;
            PUKey = KeyCode.Alpha3;
        }
        /// <summary>
        /// Same as base but adds keycode and associated game object
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_object"></param>
        public AmmoPU(TMP_Text _t, GameObject _object) : base(_t){
            assocGameObject = _object;
            isText = false;
        }
        public override void CollisionBehaviour()
        {
            this.Count++;
        
        }
        public override bool ActiveAction(){
            AmmoNums--;
            UpdateText();
            return true;
        }
        public override string Name{get{return "ammo";}}
        /// <summary>
        /// Adds num to nunmber of ammo
        /// </summary>
        /// <param name="num"></param>
        public static void AddAmmo(int num){
            AmmoNums+=num;
        }
        /// <summary>
        /// Gets number of ammos
        /// </summary>
        public static int Nums{
            get{return AmmoNums;}
        }
        public override int Count{
            get {return Nums;}
            set {AddAmmo(value);}
        }
        public override bool CanInc{
            get {return Count>0;}
        }
    }

    public class DamagePU:Powerups{
        int time;
        int increase;
        static int damageNums = 0;
        /// <summary>
        /// Same as base but adds keycode
        /// </summary>
        /// <param name="_t"></param>
        public DamagePU(TMP_Text _t) : base(_t)
        {PUKey = KeyCode.Alpha1;}
        /// <summary>
        /// Same as base but adds keycode and increment and time
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_inc"></param>
        public DamagePU(TMP_Text _t, int _inc, int _time) : base(_t){
            increase = _inc;
            time = _time;
            PUKey = KeyCode.Alpha1;
        }
        /// <summary>
        /// Same as base but adds keycode and associated game object
        /// </summary>
        /// <param name="_t"></param>
        /// <param name="_object"></param>
        public DamagePU(TMP_Text _t, GameObject _object) : base(_t){
            assocGameObject = _object;
            isText = false;
        }
        public override void CollisionBehaviour()
        {
            this.Count++;
        
        }
        public override bool ActiveAction(){
            damageNums--;
            UpdateText();
            return true;
        }
        public override string Name{get{return "damage";}}
        /// <summary>
        /// adds num to current damage
        /// </summary>
        /// <param name="num"></param>
        public static void AddDamage(int num){
            damageNums++;
        }
        /// <summary>
        /// Gets current nums
        /// </summary>
        public static int Nums{
            get{return damageNums;}
        }
        public override int Count{
            get {return Nums;}
            set {AddDamage(value);}
        }
        public override bool CanInc{
            get {return Count>0;}
        }
    }
}