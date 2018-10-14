using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{
    public Text modeText;

    public GameObject preview;

    void Start() 
    {

    }
    
    void Update() 
    {

    }

    public void changeText() 
    {
        modeText.text = (modeText.text == "Edit Mode") ? "View Mode" : "Edit Mode";
    }
}