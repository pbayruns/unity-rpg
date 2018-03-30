using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

    public QuestManager qMan;
    public int questNumber;

    public string startText;
    public string endText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartQuest()
    {
        qMan.ShowQuestText(startText);
    }

    public void EndQuest()
    {
        qMan.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
        qMan.ShowQuestText(endText);
    }
}
