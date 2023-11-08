using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameNameSpace
{
    public class GameOver : MonoBehaviour
    {
        public GameObject WinScreen;
        public GameObject OverScreen;
        bool winBool = false;
        // Start is called before the first frame update
        void Start()
        {
            WinScreen.SetActive(false);
            OverScreen.SetActive(false);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (winBool)
            {
                Debug.Log("Container Success");
                WinScreen.SetActive(true);
                TimeController.TogglePause(true);
            }
            else if (BarsFunction.HealthBarVar.CValue <= 0 || BarsFunction.AmmoBarVar.CValue <= 0)
            {
                OverScreen.SetActive(true);
                TimeController.TogglePause(true);
            }
        }

        public static void Quit()
        {
            Application.Quit();
        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Container Collide");
            if (other.gameObject.name.ToLower().Contains("first person"))
            {
                if (this.gameObject.name.ToLower().Contains("container"))
                {
                    winBool = true;
                }
            }
        }
    }
}