/*
 * Store the questions enabler
 */

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
        //set all to inactive on wake
        foreach (var panel in _panels) panel.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        //Initialize is show
        isShow = new bool[] { false, false };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Set panel activity to appropriate isshow
        _panels[0].SetActive(isShow[0]);
        _panels[1].SetActive(isShow[1]);

        //If any trues, pause game
        if (HasATrue(isShow)) TimeController.TogglePause(true);
    }
    /// <summary>
    /// Disable a provided value
    /// </summary>
    /// <param name="param"></param>
    public static void SetNo(int param)
    {
        param--;
        isShow[param] = false;
    }
    /// <summary>
    /// Check whether any in bool list are true
    /// </summary>
    /// <param name="bools"></param>
    /// <returns></returns>
    public bool HasATrue(bool[] bools)
    {
        foreach (bool s in bools) if (s) return true;
        return false;
    }
    /// <summary>
    /// on Colliding
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        //If colliding with player
        if (other.gameObject.name.ToLower().Contains("first person"))
        {
            //Show appropriate question based on name
            string name = this.gameObject.name.ToLower();
            if (name.Contains("question1")) isShow[0] = true;
            else if (name.Contains("question2")) isShow[1] = true;
        }
    }
}
