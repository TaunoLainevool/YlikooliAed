using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCmovement : MonoBehaviour
{
    public GameObject TargetNPC;
    public GameObject NPCTargetPosition;
    public int waitTime;//How long the npc stays in position before moving back
    public float moveSpeed;
    private bool movingToTarget = true;
    public bool isWaiting;

    private Vector3 moveCoords;
    private Vector3 startCoords;

    // Start is called before the first frame update
    void Start()
    {
        moveCoords = NPCTargetPosition.transform.position;
        startCoords = TargetNPC.transform.position;
        SpriteRenderer spriteRenderer = NPCTargetPosition.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        // moveToPos();

    }

    IEnumerator wait()
    {
        //Debug.Log("waiting");
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    void moveToPos()
    {
        TargetNPC.transform.position = Vector3.MoveTowards(TargetNPC.transform.position, moveCoords, moveSpeed * Time.deltaTime);
        
    }

    void moveBack()
    {
        TargetNPC.transform.position = Vector3.MoveTowards(TargetNPC.transform.position, startCoords, moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
{
    float distanceToTarget = Vector3.Distance(TargetNPC.transform.position, moveCoords);
    float distanceToStart = Vector3.Distance(TargetNPC.transform.position, startCoords);
    float threshold = 0.01f; // Small threshold for position comparison

    if (!isWaiting)
    {
        if (movingToTarget && distanceToTarget < threshold)
        {
            StartCoroutine(wait());
            movingToTarget = false;
        }
        else if (!movingToTarget && distanceToStart < threshold)
        {
            StartCoroutine(wait());
            movingToTarget = true;
        }
    }

    if (movingToTarget && !isWaiting)
    {
        moveToPos();
    }
    else if (!movingToTarget && !isWaiting)
    {
        moveBack();
    }
}

}
