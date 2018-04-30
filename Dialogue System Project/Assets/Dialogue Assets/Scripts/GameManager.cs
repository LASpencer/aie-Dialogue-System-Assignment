using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dialogue;

public class GameManager : MonoBehaviour {

    [SerializeField]
    DialogueManager dialogue;

    [SerializeField]
    DialogueActor player;
    [SerializeField]
    DialogueActor skinner;
    [SerializeField]
    DialogueActor mother;

	// Use this for initialization
	void Start () {
        dialogue.AssignActor("superintendent", player);
        dialogue.AssignActor("principal", skinner);
        dialogue.AssignActor("mother", mother);

        dialogue.StartConversation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
