using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class ResponseButtonStrategy : UIDisplayStrategy
    {
        [SerializeField]
        RectTransform namePanel;
        [SerializeField]
        Text nameText;

        [SerializeField]
        RectTransform dialoguePanel;
        [SerializeField]
        Text dialogueText;

        [SerializeField]
        Button NextButton;

        [SerializeField]
        GameObject ButtonPrototype;

        private void Awake()
        {
            uiManager = gameObject.GetComponent<DialogueUI>();
        }

        private void Start()
        {
            NextButton.onClick.AddListener(uiManager.manager.NextEntry);
        }

        public override void ClearResponses()
        {
            base.ClearResponses();
            // TODO show "Next button"
            //TODO clear Response buttons
        }

        public override void DisplayDialogueEntry(DialogueEntry entry)
        {
            base.DisplayDialogueEntry(entry);
            //TODO resize panels to fit name, entry, possible responses
            //TODO update text
        }

        public override void DisplayResponse(Response response, int ID, bool possible)
        {
            base.DisplayResponse(response, ID, possible);
            //TODO if necessary resize panel to fit new response
            //TODO spawn new button with action to select appropriate response
        }

        public override void OnConversationEnd()
        {
            base.OnConversationEnd();
        }

        public override void OnConversationStart()
        {
            base.OnConversationStart();
        }
    }
}
