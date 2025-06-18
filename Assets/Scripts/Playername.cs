using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Playername : MonoBehaviour
{
    public TMP_InputField playerInput;
    
    public string playerName;
    public void saveName()
    {
        playerName = playerInput.text;
    }
}
