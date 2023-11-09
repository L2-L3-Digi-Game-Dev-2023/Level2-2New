using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{

public class Enemies{
    static List<Enemy> enemies = new List<Enemy>();

    public static List<Enemy> EnemiesList{
        get=> enemies;
        set=> enemies = value;
    }
    public static new string ToString()
        {
            string output = "";
            foreach(Enemy enemy in enemies)
            {
                output += $"{enemy}\n";
            }
            return output;
        }
}
public class Enemy
{
    bool moving = true;
    GameObject assoc;
    public Enemy(){}
    public Enemy(GameObject assoc){
        this.assoc = assoc;
    }
    public bool Moving{
        get=> moving;
        set=> moving = value;
    }
    public GameObject AssocGO{
        get=> assoc;
        set => assoc = value;
    }

    public override string ToString()
        {
            string output = $"{assoc.name} {moving.ToString()}";
            return output ;
        }
}
}