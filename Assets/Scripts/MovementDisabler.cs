using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDisabler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public void disableMovement()
    {
        playerMovement.canMove = false;
    }

    public void enableMovement()
    {
        playerMovement.canMove = true;
    }
}
