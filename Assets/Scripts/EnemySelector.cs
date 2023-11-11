/*
 * Code to select an enemy from the list according to parameters
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace GameNameSpace{
    public class EnemySelector : MonoBehaviour
    {
        //list of enemies
        static List<GameObject> EnemiesList;
        //List of animators
        static List<Animator> animList = new List<Animator>();
        // Start is called before the first frame update
        void Start()
        {
            //initialize list of enemies as a list of gameobjects
            EnemiesList = new List<GameObject>();
            //For each child object (as this is on a shared parent of all enemies
            foreach(Transform child in transform){
                //Add gameobject to list
                EnemiesList.Add(child.gameObject);
            }
            //If list is geq 1
            if(EnemiesList.Count >= 1){
                //Add all animator components to list
                for(int i = 0; i < EnemiesList.Count - 1; i++){
                    animList.Add(EnemiesList[i].GetComponent<Animator>());

                }
            }
        }
        /// <summary>
        /// Find an enemy in list
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="agent"></param>
        public static void FindEnemy(Enemy enemy, NavMeshAgent agent)
        {
            //If > 0 ammo
            if (!(BarsFunction.AmmoBarVar.CValue <= 0))
            {
                //decrease height of enemy 
                enemy.AssocGO.transform.position -= new Vector3(0f, 1f, 0f);
                //If no enemies in enemieslist return
                if (EnemiesList.Count == 0) return;
                // else if singular
                else if (EnemiesList.Count == 1)
                {
                    //Set die to true of that animator
                    animList[0].SetBool("isDie", true);

                }
                //Else if > 1
                else if (EnemiesList.Count > 1)
                {
                    //for every gameobject in the enemy list
                    for (int i = 0; i < EnemiesList.Count - 1; i++) 
                    {
                        //If matches enemy gameobject from param
                        if (EnemiesList[i] == enemy.AssocGO)
                        {
                            //Set the animation list bool to true
                            animList[i].SetBool("isDie", true); 
                        }
                    }
                }
                //Else if none conditions return
                else
                {
                    return;
                }
            }
            else return;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}