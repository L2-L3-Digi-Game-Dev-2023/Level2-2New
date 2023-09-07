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
    public Powerups(){
        maxColor = new Color(1f,0.48f,0f);
        defColor = new Color(1f,1f,1f);
    }
    public Powerups(TMP_Text _t){
        text = _t;
        maxColor = new Color(1f,0.48f,0f);
        defColor = new Color(1f,1f,1f);
        isText = true;
    }

    ~Powerups(){
        Debug.Log("Powerup destroyed");
    }

    public abstract void CollisionBehaviour();

    public bool IsText {
        get {
            if (!isText == null) return isText;
            else return false;
        }
    }
    public abstract string Name {get;}
    public KeyCode Key{
        get {return PUKey;}
    }

    public string Text{
        get{return text.text;}
        set{text.text = value;}
    }

    public int Count{
        get{return count;}
        set{
            if (!(value >= MAX_COUNT)) count = value;
            else {
                count = 10;
                isMax = true;
            }
        }
    }
    public GameObject associatedObject{
        get {return assocGameObject;}
    }
    public void UpdateText(){
        Text = Count.ToString();
    }
    

    public void SetColor(){
        text.color = isMax ? maxColor : defColor;
    }

}
public class HealthPU : Powerups{
    int increase;
    public HealthPU(TMP_Text _t) : base(_t)
    {PUKey = KeyCode.Alpha2;}
    public HealthPU(TMP_Text _t, int _inc) : base(_t)
    {
        increase = _inc;
        PUKey = KeyCode.Alpha2;
    }
    public HealthPU(TMP_Text _t, GameObject _object) : base(_t){
        assocGameObject = _object;

        isText = false;
    }
    public override string Name{get{return "heart";}}

    public override void CollisionBehaviour()
    {
        this.Count++;
        
    }
}

public class AmmoPU : Powerups{
    int increase;
    public AmmoPU(TMP_Text _t) : base(_t)
    {PUKey = KeyCode.Alpha3;}
    public AmmoPU(TMP_Text _t, int _inc) : base(_t)
    {
        increase = _inc;
        PUKey = KeyCode.Alpha3;
    }
    public AmmoPU(TMP_Text _t, GameObject _object) : base(_t){
        assocGameObject = _object;
        isText = false;
    }
    public override void CollisionBehaviour()
    {
        this.Count++;
        
    }
    public override string Name{get{return "ammo";}}

}

public class DamagePU:Powerups{
    int time;
    int increase;
    public DamagePU(TMP_Text _t) : base(_t)
    {PUKey = KeyCode.Alpha1;}
    public DamagePU(TMP_Text _t, int _inc, int _time) : base(_t){
        increase = _inc;
        time = _time;
        PUKey = KeyCode.Alpha1;
    }
    public DamagePU(TMP_Text _t, GameObject _object) : base(_t){
        assocGameObject = _object;
        isText = false;
    }
    public override void CollisionBehaviour()
    {
        this.Count++;
        
    }
    public override string Name{get{return "damage";}}

}
}