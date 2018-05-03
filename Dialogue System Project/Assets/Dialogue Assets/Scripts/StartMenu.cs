using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartMenu : MonoBehaviour {

    [SerializeField]
    Dialogue.LocalizationManager localizer;

    [SerializeField]
    Text titleText;
    [SerializeField]
    Text startText;
    [SerializeField]
    Text languageText;

    public GameManager manager;

    public UnityAction StartGame;

    [SerializeField]
    List<string> localeCodes;

	// Use this for initialization
	void Start () {
        Invoke("LocalizeMenu", 0.01f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LanguageChanged(Dropdown change)
    {
        localizer.SetLocale(localeCodes[change.value]);
        LocalizeMenu();
    }

    public void OnPressPlay()
    {
        StartGame.Invoke();
    }

    void LocalizeMenu()
    {
        titleText.text = localizer.GetLine("menu_title");
        startText.text = localizer.GetLine("menu_play");
        languageText.text = localizer.GetLine("menu_language");
    }
}
