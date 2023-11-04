using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameNameSpace
{
    public class BulletChecker : MonoBehaviour
    {
        HealthBar health;
        AmmoBar ammo;
        // Start is called before the first frame update
        void Start()
        {
            health = (HealthBar)(BarsFunction.bars[0]);
            ammo = (AmmoBar)(BarsFunction.bars[1]);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name.ToLower().Contains("bullet"))
            {
                Debug.Log("REUIAHFUJNE");
                health.Decrease(5);
                Destroy(other.gameObject);
            }
        }
    }
}