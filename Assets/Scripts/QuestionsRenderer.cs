using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class QuestionsRenderer : MonoBehaviour
{
    public GameObject[] _panels;
    public static bool[] isShow;

    void Awake()
    {
        foreach (var panel in _panels) panel.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        isShow = new bool[] { false, false };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _panels[0].SetActive(isShow[0]);
        _panels[1].SetActive(isShow[1]);

        Debug.Log(HasATrue(isShow));
        if (HasATrue(isShow)) TimeController.TogglePause(true);
    }
    public static void SetNo(int param)
    {
        param--;
        isShow[param] = false;
    }

    public bool HasATrue(bool[] bools)
    {
        foreach (bool s in bools) if (s) return true;
        return false;
    }
    //public static void Hide()
    //{
    //    Active = false;
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //public static void Show()
    //{
    //    Active = true;
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("TESTSETETSEDTGESTG");
        if (other.gameObject.name.ToLower().Contains("first person"))
        {
            string name = this.gameObject.name.ToLower();
            if (name.Contains("question1")) isShow[0] = true;
            else if (name.Contains("question2")) isShow[1] = true;
            else Debug.Log("INCORREVENRHOGIN");
        }
    }
}
