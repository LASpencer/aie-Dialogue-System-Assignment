using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue {
    public class DialogueUI : MonoBehaviour
    {
        // TODO figure out good interface
        public DialogueManager manager;
        public CutsceneManager cutsceneManager;
        public UIDisplayStrategy displayStrategy;

        private void Awake()
        {
            if (displayStrategy == null)
            {
                displayStrategy = gameObject.GetComponent<UIDisplayStrategy>();
            }
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void OnConversationStart()
        {
            SetDialogueEntry(manager.current);
        }

        public void SetDialogueEntry(DialogueEntry entry)
        {
            displayStrategy.DisplayDialogueEntry(entry);
            displayStrategy.ClearResponses();
            if (entry.Responses.Count > 0)
            {
                for (int i = 0; i < entry.Responses.Count; ++i)
                {
                    Response response = entry.Responses[i];
                    displayStrategy.DisplayResponse(response, i, response.CheckPrerequisite(manager));
                }
            }
            // Inform display strategy all responses have been sent
            displayStrategy.FinishDisplayDialogueEntry();
        }

        public void OnConversationEnd()
        {
            displayStrategy.OnConversationEnd();
        }
    }
}
