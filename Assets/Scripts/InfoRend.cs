/*
 * Render info panel
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class InfoRend : MonoBehaviour
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
        isShow = new bool[] { false, false, false };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Set to appropriate bools
        _panels[0].SetActive(isShow[0]);
        _panels[1].SetActive(isShow[1]);
        _panels[2].SetActive(isShow[2]);
        //if any of the isshow's are true, pause
        if (HasATrue(isShow)) TimeController.TogglePause(true);
    }
    /// <summary>
    /// open particular panel
    /// </summary>
    /// <param name="param"></param>
    public static void SetNo(int param)
    {
        param--;
        isShow[param] = false;
    }
    /// <summary>
    /// Check if list has a true
    /// </summary>
    /// <param name="bools"></param>
    /// <returns></returns>
    public bool HasATrue(bool[] bools)
    {
        foreach (bool s in bools) if (s) return true;
        return false;
    }
    /// <summary>
    /// Check collisions
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        //if colliding with player
        if (other.gameObject.name.ToLower().Contains("first person"))
        {
            string name = this.gameObject.name.ToLower();
            //Set appropriate true depending on current object name 
            if (name.Contains("info")) isShow[0] = true;
            else if (name.Contains("incorrect")) isShow[1] = true;
            else if (name.Contains("correct")) isShow[2] = true;
        }
    }

}
