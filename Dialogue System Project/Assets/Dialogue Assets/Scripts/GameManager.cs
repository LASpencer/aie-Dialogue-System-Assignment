using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Dialogue;

public class GameManager : MonoBehaviour {

    [SerializeField]
    DialogueManager dialogue;
    [SerializeField]
    StartMenu startMenu;

    private void Awake()
    {
        startMenu.manager = this;
        startMenu.StartGame = this.StartGame;
    }

    // Use this for initialization
    void Start () {
        dialogue.OnConversationEnd.AddListener(EndGame);
        dialogue.cutsceneManager.DeactivateAnimators();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame()
    {
        dialogue.cutsceneManager.ActivateAnimators();
        dialogue.StartConversation();
        startMenu.gameObject.SetActive(false);
    }

    void EndGame()
    {
        Application.Quit();
    }
}
