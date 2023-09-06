/*
* NAME: Hayden Gillanders
* DATE MODIFIED: 5 SEPT 2023
* MODIFICATION: Attempting collision functionality
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameNameSpace{
public class PowerupCollisions : PowerUpController
{
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(GameObject _object in gObjs){
            if (_object == this){
                if (this.name.Contains("ammo")) powerups.Add(new AmmoPU(this.gameObject));
                else if(this.name.Contains("health")) powerups.Add(new HealthPU(this.gameObject));
                else if(this.name.Contains("damage")) powerups.Add(new DamagePU(this.gameObject));
                Debug.Log(this.name);
            }
        }
    }
    //iydugf
//testf
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
        {
            foreach(Powerups power in powerups.ToList()){
                if (power.associatedObject != null){
                    power.CollisionBehaviour();
                    Destroy(power.associatedObject);
                    powerups.Remove(power);
                }
            }
        }
}
}