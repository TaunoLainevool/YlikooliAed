using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public float headRed, headGreen, headBlue;
    public float bodyRed, bodyGreen, bodyBlue;
    private GameObject headObj;
    private GameObject bodyObj;

    private SpriteRenderer bodyColor;
    private SpriteRenderer headColor;
    // Start is called before the first frame update
    void GetPlayerSprites()
    {
        headObj = GameObject.FindGameObjectWithTag("Head");
        bodyObj = GameObject.FindGameObjectWithTag("Body");

        bodyColor = bodyObj.GetComponent<SpriteRenderer>();
        headColor = headObj.GetComponent<SpriteRenderer>();
        // Debug.Log(headColor);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(headObj);
        if (headObj == null)
        {
            GetPlayerSprites();
        }

        headColor.color = new Color(1 - headRed, 1 - headGreen, 1 - headBlue);
        bodyColor.color = new Color(1 - bodyRed, 1 - bodyGreen, 1 - bodyBlue);
        
    }
}
