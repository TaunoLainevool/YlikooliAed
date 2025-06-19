using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DBNPC : MonoBehaviour, IInteractible
{
    public DBdialogue dialogueData;
    private DialogueController dialogueUI;

    public int dialogueIndex; //may break something, if does - remove public
    private bool isTyping, isDialogueActive;
    
    private bool hasGivenPlant = false;

    public GameObject plantPrefab;

    public GameObject replacementNPC;

    public GameObject replaceableNPC;

    public NPCmovement npcMovement;
    public string npcName;
    public Sprite npcSprite;
    MovementDisabler movementDisabler;

    public int[] DbQuestionIndex;
    
    PointController pointController;

    private string optionalText;

    private int[] usableDialogues = new int[999];
    private string[] usableDialogueLines=new string[999];
    private string[][] usableChoices = new string[999][];
    
    private int tempIndex;
    private string NPCtag;
    private int helpMEForGodsSake = 0;

    //List<Questions> questionsFromDB = DBconnection.Instance.questionList;


    private void Start()
    {
        //Debug.Log(questionsFromDB);
        hasGivenPlant = false;
        dialogueUI = DialogueController.Instance;
        GameObject pointControllerObj = GameObject.FindGameObjectWithTag("PointController");
        pointController = pointControllerObj.GetComponent<PointController>();

        GameObject movementDisablerObj = GameObject.FindGameObjectWithTag("Movement disabler");
        movementDisabler = movementDisablerObj.GetComponent<MovementDisabler>();
        NPCtag = gameObject.tag;

        for (int i = 0; i < dialogueData.dialogueLines.Length; i++)
        {
            // Debug.Log(i);
            // Debug.Log(dialogueData.gameTitle[i]);
            if (dialogueData.gameTitle[i] == NPCtag)
            {
                Debug.Log("activate");

                usableDialogueLines[tempIndex] = dialogueData.dialogueLines[i];
                Debug.Log(usableDialogueLines[tempIndex]);
                usableChoices[tempIndex] = dialogueData.choices[i].choices;
                Debug.Log(usableChoices[tempIndex][0]);
                Debug.Log(usableChoices[tempIndex][1]);
                Debug.Log(usableChoices[tempIndex][2]);
                Debug.Log(usableChoices[tempIndex][3]);
                usableDialogues[tempIndex] = i;
                Debug.Log(usableDialogues[tempIndex]);
                ++tempIndex;
                Debug.Log(dialogueData.dialogueLines[0]);
            }
        }


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
            if (npcMovement != null)
            {
                npcMovement.isWaiting = true;
            }
            StartDialogue(0);
        }
    }
    
    public bool CanInteract()
    {
        return !isDialogueActive;
    }
    public void StartDialogue(int index){ //may break something, if does - remove public
        isDialogueActive = true;
        // Debug.Log(isDialogueActive);
        dialogueIndex =index;
        dialogueUI.SetNPCInfo(npcName, npcSprite);
        dialogueUI.ShowDialogueUI(true);
        movementDisabler.disableMovement();
        DisplayCurrentLine();

    }
    void NextLine(){
        
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(usableDialogueLines[DbQuestionIndex[dialogueIndex]]); //dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
            // Debug.Log(dialogueData.gameTitle[dialogueIndex]);
            // Debug.Log(dialogueData.choices[dialogueIndex].correctAnswers[0]);
            // Debug.Log(dialogueData.choices[dialogueIndex].correctAnswers[1]);
            // Debug.Log(dialogueData.choices[dialogueIndex].correctAnswers[2]);
            // Debug.Log(dialogueData.choices[dialogueIndex].correctAnswers[3]);
        }
        
        dialogueUI.ClearChoices();

        

        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        

        foreach (DialogueChoice dialogueChoice in dialogueData.choices) 
        {
            if (dialogueChoice.dialogueIndex == usableDialogues[DbQuestionIndex[dialogueIndex]]/*dialogueIndex*/)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if(++dialogueIndex < usableDialogueLines.Length /*dialogueData.dialogueLines.Length*/){
            DisplayCurrentLine();
        }
        else{
            EndDialogue();
        }
    }
    IEnumerator TypeLine() {
        isTyping = true;
        dialogueUI.SetDialogueText("");
        foreach (char letter in usableDialogueLines[DbQuestionIndex[dialogueIndex]] /*dialogueData.dialogueLines[dialogueIndex]*/)
        {

            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }



void DisplayChoices(DialogueChoice choice){
        for (int i = 0; i < choice.choices.Length; i++) {
            int nextIndex = /*choice.nextDialogueIndexes[i]*/ dialogueIndex+1;
            // Debug.Log(i);
            // Debug.Log(choice.correctAnswers[i]);
            int choiceIndex = i;
            dialogueUI.CreateChoiceButton(choice.choices[i], () =>
            {
                if (choice.isPointable)
                {
                    if (choice.correctAnswers[choiceIndex] == true)
                    {
                        // Debug.Log("correct answer");
                        pointController.addPoints(choice.choices.Length);
                        // Debug.Log(pointController.getPoints());
                    }
                    else
                    {
                        pointController.removePoints(1);
                        // Debug.Log("incorrect");
                    }
                }

                
                ChooseOption(nextIndex);
            }
                );
        
    }
}

void ChooseOption(int nextIndex){
        helpMEForGodsSake += 1;
        // if (usableDialogueLines[dialogueIndex+1] == null)
        if (usableDialogueLines[DbQuestionIndex[dialogueIndex] + 1] == null || helpMEForGodsSake >= DbQuestionIndex.Length)
        {
            replaceNPC();
            EndDialogue();
            givePlant();
            return;
        }
    dialogueIndex = nextIndex;
    dialogueUI.ClearChoices();
    DisplayCurrentLine();
}

void DisplayCurrentLine(){
    StopAllCoroutines();
    StartCoroutine(TypeLine());
}

    public void EndDialogue()
    {
        StopAllCoroutines();
        // Debug.Log(isDialogueActive);
        isDialogueActive = false;
        // Debug.Log(isDialogueActive);
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);
        dialogueUI.ClearChoices();
        givePlant();
        // Debug.Log("Passed given plant");
        replaceNPC();
        // Debug.Log("Passed replacement");
        movementDisabler.enableMovement();
        if (npcMovement != null)
        {
            npcMovement.isWaiting = false;
        }

    }

    void replaceNPC(){
        // Debug.Log(dialogueIndex +" "+ dialogueData.dialogueLines.Length);
        Debug.Log(dialogueIndex+" "+dialogueData.dialogueLines.Length);
        // if (dialogueIndex >= dialogueData.dialogueLines.Length-1)
        // {
            if (replacementNPC == null && replaceableNPC == null)
            {
                Debug.Log("There is a nully wully");
                return;
            }
            else
            {
                Debug.Log("No nully wully");
                GameObject oldNPC = replaceableNPC;
                // Debug.Log("oldnpc "+oldNPC);
                // Debug.Log("replaced");
                replacementNPC.SetActive(true);
                // Debug.Log("new here");
                oldNPC.SetActive(false);
                // Debug.Log("old gone");
            }
        // }
    }

    void givePlant(){
        //Debug.Log(dialogueData.dialogueLines.Length);
        if(/*dialogueIndex == dialogueData.dialogueLines.Length && */dialogueData.plantGiver == true /*&& hasGivenPlant !=true*/){
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



