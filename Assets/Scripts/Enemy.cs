/*
 * File for storing Enemies and Enemy Class information
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace{

    public class Enemies{
        //List of enemies
        static List<Enemy> enemies = new List<Enemy>();
        /// <summary>
        /// publically accessible lists of enemies
        /// </summary>
        public static List<Enemy> EnemiesList{
            get=> enemies;
            set=> enemies = value;
        }
        /// <summary>
        /// Output the list as a string 
        /// </summary>
        /// <returns></returns>
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
        //create movign bool and set def to true
        bool moving = true;
        //Create gameobject var
        GameObject assoc;
        /// <summary>
        /// empty constructor with no function
        /// </summary>
        public Enemy(){}
        /// <summary>
        /// Constructor with gameobject parameter to initalize associated to param
        /// </summary>
        /// <param name="assoc"></param>
        public Enemy(GameObject assoc){
            this.assoc = assoc;
        }
        /// <summary>
        /// property to access moving variable
        /// </summary>
        public bool Moving{
            get=> moving;
            set=> moving = value;
        }
        /// <summary>
        /// Property to access the object's assocaited game object
        /// </summary>
        public GameObject AssocGO{
            get=> assoc;
            set => assoc = value;
        }
        /// <summary>
        /// Override object.ToString() to show correctly formatted with object name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            {
                string output = $"{assoc.name} {moving.ToString()}";
                return output ;
            }
    }
}