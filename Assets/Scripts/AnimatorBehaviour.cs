using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{
    Animator animateComp;
    // Start is called before the first frame update
    void Start()
    {
        animateComp = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            Debug.Log("REGISTER");
            switch(animateComp.GetBool("isRun")){
                case true:
                    animateComp.SetBool("isRun", false);
                    break;
                case false:
                    animateComp.SetBool("isRun", true);
                    break;
            }
        }
    }
}
