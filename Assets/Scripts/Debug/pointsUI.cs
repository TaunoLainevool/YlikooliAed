using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pointsUI : MonoBehaviour
{
    public TMP_Text pointText;
    public bool isActive;
    public PointController pointController;

    void Start()
    {
        
    }
    void Update()
    {
        pointText.enabled = isActive;
        pointText.text = "punktid:"+ pointController.getPoints();
    }
}
