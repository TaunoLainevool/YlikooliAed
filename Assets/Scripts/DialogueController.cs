using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance{get; private set;}
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;

    

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
   }

   public void SetNPCInfo(string npcName, Sprite portrait){
        nameText.text = npcName;
        portraitImage.sprite = portrait;
   }

   public void SetDialogueText(string text){
    dialogueText.text = text;
   }

   public void ClearChoices(){
    foreach(Transform child in choiceContainer){
        Destroy(child.gameObject);
    }
   }

   public GameObject CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick) {
    GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
    choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
    choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    return choiceButton;
   }
}
