using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace GameNameSpace{
public class EnemySelector : MonoBehaviour
{

    static List<GameObject> EnemiesList;
    static List<Animator> animList = new List<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        EnemiesList = new List<GameObject>();
        foreach(Transform child in transform){
            EnemiesList.Add(child.gameObject);
        }
        if(EnemiesList.Count >= 1){
            for(int i = 0; i < EnemiesList.Count - 1; i++){
                animList.Add(EnemiesList[i].GetComponent<Animator>());

            }
        }
    }

    public static void FindEnemy(Enemy enemy, NavMeshAgent agent)
    {
            if (EnemiesList.Count == 0) return;
        else if (EnemiesList.Count == 1)
            {
                animList[0].SetBool("isDie", true);
                if (agent != null) agent.baseOffset = 1;
            }
        else if (EnemiesList.Count > 1)
        {
                if (agent != null) agent.baseOffset = 1;
                for (int i = 0; i < EnemiesList.Count -1; i++) //NOTE: do “EnemiesList.Count - 1” instead, if you get index out of range error
            {
                if(EnemiesList[i] == enemy.AssocGO)
                {
                        //DO STUFF HERE
                    animList[i].SetBool("isDie", true); //set the animator parameter to play the animation[i]
                                                        //Remember to turn off this specific animator to avoid turning when another valve is activated. i = the number of the animator in the list. if in the inspector it says: “Element 0” then this would be the same as “animList[0
                }
            }
        }
        
        else
        {
        return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}