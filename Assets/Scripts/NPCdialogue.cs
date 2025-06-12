using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewNPCDialogue", menuName ="NPC dialogue")]
public class NPCdialogue : ScriptableObject
{


    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines; //Mark where dialogue ends
    public float autoProgressDelay = 1.5f;
    public bool plantGiver = true; //NPC gives plants

    
    public DialogueChoice[] choices;
}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex;//Dialogue line where choices appear
    public string[] choices;//Player response options
    public int[] nextDialogueIndexes;//Where choice leads
    public bool isPointable;
    public bool[] correctAnswers;//What answers give points
  
}