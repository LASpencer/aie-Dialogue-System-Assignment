using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartMenu : MonoBehaviour {

    [SerializeField]
    Dialogue.LocalizationManager localizer;

    public GameManager manager;

    public UnityAction StartGame;

    [SerializeField]
    List<string> localeCodes;

	// Use this for initialization
	void Start () {
        //TODO localize title + button text
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LanguageChanged(Dropdown change)
    {
        localizer.SetLocale(localeCodes[change.value]);
        //TODO reload title + button text from localizer
    }

    public void OnPressPlay()
    {
        StartGame.Invoke();
    }
}
