using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractible
{
    public NPCdialogue dialogueData;
    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    
    private bool hasGivenPlant = false;

    public GameObject plantPrefab;

    public GameObject replacementNPC;

    


    private void Start()
    {
        hasGivenPlant = false;
        dialogueUI = DialogueController.Instance;

    }
    public void Interact(){
        if(dialogueData == null){
            return;
        }
        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    
    public bool CanInteract()
    {
        return !isDialogueActive;
    }
    void StartDialogue(){
        
        isDialogueActive = true;
        dialogueIndex =0;
        dialogueUI.SetNPCInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUI.ShowDialogueUI(true);
        DisplayCurrentLine();

    }
    void NextLine(){
        if(isTyping){
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping=false;
        }
        
        dialogueUI.ClearChoices();

        if(dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex]){
            EndDialogue();
            return;
        }

        foreach(DialogueChoice dialogueChoice in dialogueData.choices){
            if(dialogueChoice.dialogueIndex == dialogueIndex) {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if(++dialogueIndex < dialogueData.dialogueLines.Length){
            DisplayCurrentLine();
        }
        else{
            EndDialogue();
        }
    }
    IEnumerator TypeLine(){
        isTyping = true;
        dialogueUI.SetDialogueText("");
        foreach(char letter in dialogueData.dialogueLines[dialogueIndex]){
            
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text+=letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]){
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

void DisplayChoices(DialogueChoice choice){
    for(int i = 0; i<choice.choices.Length; i++){
        int nextIndex = choice.nextDialogueIndexes[i];
        dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
    }
}

void ChooseOption(int nextIndex){
    dialogueIndex = nextIndex;
    dialogueUI.ClearChoices();
    DisplayCurrentLine();
}

void DisplayCurrentLine(){
    StopAllCoroutines();
    StartCoroutine(TypeLine());
}

    public void EndDialogue(){
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        givePlant();
        // Debug.Log("Passed given plant");
        replaceNPC();
        // Debug.Log("Passed replacement");
    }

    void replaceNPC(){
        // Debug.Log(dialogueIndex +" "+ dialogueData.dialogueLines.Length);
        if(dialogueIndex+1 == dialogueData.dialogueLines.Length){
            if (replacementNPC == null){
                // Debug.Log("There is a nully wully");
                return;
            } 
            else{
                // Debug.Log("No nully wully");
                GameObject oldNPC = GameObject.FindGameObjectWithTag("Roheklubi juht");
                // Debug.Log("oldnpc "+oldNPC);
                // Debug.Log("replaced");
                replacementNPC.SetActive(true);
                oldNPC.SetActive(false);
            }
        }
    }

    void givePlant(){
        Debug.Log(dialogueData.dialogueLines.Length);
        if(dialogueIndex == dialogueData.dialogueLines.Length && dialogueData.plantGiver == true && hasGivenPlant !=true){
            if(plantPrefab){
                GameObject player;
                player = GameObject.FindGameObjectWithTag("Player");

                GameObject obtainedPlant = Instantiate(plantPrefab, player.transform.position + Vector3.up, Quaternion.identity);
                Debug.Log(player.transform.position);
                Debug.Log(player.transform.position + Vector3.up);
                obtainedPlant.transform.SetParent(player.transform);
                //GameObject droppedItem = Instantiate(plantPrefab, this.transform);
                hasGivenPlant = true;
                Debug.Log(obtainedPlant);
        }
        
        
    }
}
    
}



