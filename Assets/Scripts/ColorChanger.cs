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
    void Start()
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
        headColor.color = new Color(headRed, headGreen, headBlue);
        bodyColor.color = new Color(bodyRed, bodyGreen, bodyBlue);
    }
}
