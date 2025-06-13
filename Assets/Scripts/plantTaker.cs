using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plantTaker : MonoBehaviour, IInteractible
{
    bool takesPlants = true;
    GameObject obtainedPlant;
    GameObject lavenderPlant;
    GameObject parsleyPlant;
    GameObject tulipPlant;
    GameObject fernPlant;
    GameObject rosemaryPlant;
    GameObject peppermintPlant;
    public NPC npc;
    private int allPlants = 6;
    private int gatheredPlants = 0;
    public bool CanInteract()
    {
        return takesPlants;
    }

    public void Interact()
    {
        obtainedPlant = GameObject.FindGameObjectWithTag("Plant");
        if (gatheredPlants == allPlants)
        {
            // npc.StartDialogue(4);
            SceneManager.LoadSceneAsync("GameOver");
            Debug.Log("you diiiid iiiiiiit, gooooooooooood jooooooooooooooooobI");
        }
        else if (obtainedPlant == null)
        {
            Debug.Log("There is no plant");
            npc.StartDialogue(0);
            return;
        }
        else
        {
            Debug.Log("There is a plant");
            switch (obtainedPlant.name)
            {
                case "Lavender(Clone)":
                    // npc.StartDialogue();
                    Debug.Log("it is a lavender");
                    npc.StartDialogue(1);
                    // npc.dialogueIndex = 1;
                    lavenderPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                case "Petersell(Clone)":
                    Debug.Log("There is a parsley");
                    npc.StartDialogue(2);
                    parsleyPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                case "Tulip(Clone)":
                    Debug.Log("There is a tulip");
                    npc.StartDialogue(3);
                    tulipPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                case "Fern(Clone)":
                    Debug.Log("There is a fern");
                    npc.StartDialogue(4);
                    fernPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                case "Rosemary(Clone)":
                    Debug.Log("There is a rosemary");
                    npc.StartDialogue(5);
                    rosemaryPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                case "Peppermint(Clone)":
                    Debug.Log("There is a peppermint");
                    npc.StartDialogue(6);
                    peppermintPlant.transform.GetChild(0).gameObject.SetActive(true);
                    gatheredPlants += 1;
                    break;
                default:
                    Debug.Log("default");

                    break;
            }
            Destroy(obtainedPlant);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        lavenderPlant = GameObject.FindGameObjectWithTag("Lavender");
        parsleyPlant = GameObject.FindGameObjectWithTag("Parsley");
        tulipPlant = GameObject.FindGameObjectWithTag("Tulip");
        fernPlant = GameObject.FindGameObjectWithTag("Fern");
        rosemaryPlant = GameObject.FindGameObjectWithTag("Rosemary");
        peppermintPlant = GameObject.FindGameObjectWithTag("Peppermint");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
