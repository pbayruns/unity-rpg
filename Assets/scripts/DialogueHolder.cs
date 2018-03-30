using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    private DialogueManager dMgmt;

	// Use this for initialization
	void Start () {
        dMgmt = FindObjectOfType<DialogueManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                dMgmt.ShowBox(dialogue);
            }
        }
    }
}
