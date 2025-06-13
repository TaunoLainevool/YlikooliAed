using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointShower : MonoBehaviour
{
    public TMP_Text pointText;
    PointController pointController;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject pointControllerObj = GameObject.FindGameObjectWithTag("PointController");
        pointController = pointControllerObj.GetComponent<PointController>();

        pointText.text = pointController.playerPoints+" punkti!";
    }
}
