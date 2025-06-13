using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ColorChanger colorChanger;
    public Slider HeadRed, HeadGreen, HeadBlue;
    public Slider BodyRed, BodyGreen, BodyBlue;

    public void changeHeadRed()
    {
        colorChanger.headRed = HeadRed.value;
    }

    public void changeHeadGreen()
    {
        colorChanger.headGreen = HeadGreen.value;
    }

    public void changeHeadBlue()
    {
        colorChanger.headBlue = HeadBlue.value;
    }

    public void changeBodyRed()
    {
        colorChanger.bodyRed = BodyRed.value;
    }

    public void changeBodyGreen()
    {
        colorChanger.bodyGreen = BodyGreen.value;
    }

    public void changeBodyBlue()
    {
        colorChanger.bodyBlue = BodyBlue.value;
    }
}
