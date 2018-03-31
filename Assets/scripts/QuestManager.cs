using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public QuestObject[] quests;
    public bool[] questCompleted;

    public DialogueManager dMan;

    public string itemCollected;
    public string enemyKilled;

	// Use this for initialization
	void Start () {
        questCompleted = new bool[quests.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string text)
    {
        dMan.dialogueLines = new string[1];
        dMan.dialogueLines[0] = text;
        dMan.currentLine = 0;
        dMan.ShowDialogue();
    }
}
