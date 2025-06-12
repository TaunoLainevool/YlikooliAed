using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public int playerPoints;
    // Start is called before the first frame update
    public void addPoints(int amount)
    {
        playerPoints += amount;
    }

    public void removePoints(int amount)
    {
        if (playerPoints != 0)
            playerPoints -= amount;

    }

    public void setPoints(int amount)
    {
        playerPoints = amount;
    }

    public int getPoints()
    {
        return playerPoints;
    }
}
