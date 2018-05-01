﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dialogue;

public class GameManager : MonoBehaviour {

    [SerializeField]
    DialogueManager dialogue;


	// Use this for initialization
	void Start () {
        dialogue.StartConversation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}