using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    public GameObject startGO;
    static bool isActive = true;
    void Awake()
    {
        Debug.Log("EUIHISJFNUEJINFSE");
    }
    // Start is called before the first frame update
    void Start()
    {
        startGO.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    void LateUpdate()
    {
        if (!isActive)
        {
            startGO.SetActive(false);
            Destroy(gameObject);
        }
    }

    public static void OKButton()
    {
        isActive = false;
    }
}
