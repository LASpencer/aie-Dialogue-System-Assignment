using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue {
    public class DialogueUI : MonoBehaviour
    {
        // TODO figure out good interface
        public DialogueManager manager; 

        public Text dialogueText;
        public Text actorName;
        public Text responseText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //HACK dialogue should just have transitions out, which can be tested, and pressing
            // submit or selecting an option should just make a condition happen

            // TODO some code to tell the dialogue manager a choice is made/try to go to next
            if(manager.current != null && manager.current.Responses.Count > 0)
            {
                // HACK probably use delegates instead? Pass manager.ResponseSelected to button or whatever?
                for (int i = 0; i < manager.current.Responses.Count; ++i)
                {
                    if (Input.GetKeyDown("" + i))
                    {
                        manager.ResponseSelected(i);
                    }
                }
            } else
            {
                if (Input.GetButtonDown("Submit"))
                {
                    // HACK 
                    manager.NextEntry();
                }
            }

        }

        public void OnConversationStart()
        {
            SetDialogueEntry(manager.current);
        }

        public void SetDialogueEntry(DialogueEntry entry)
        {
            dialogueText.text = entry.Text;
            actorName.text = entry.Speaker.Name;
            if (entry.Responses.Count > 0)
            {
                // HACK do something better than this
                System.Text.StringBuilder responseTextBuilder = new System.Text.StringBuilder();
                for (int i = 0; i < entry.Responses.Count; ++i)
                {
                    responseTextBuilder.Append(i);
                    responseTextBuilder.Append(": " + entry.Responses[i].Text + "\n");
                }
                responseText.text = responseTextBuilder.ToString();
            }
            else
            {
                responseText.text = "";
            }
        }

        public void OnConversationEnd()
        {
            dialogueText.text = "THE END";
            actorName.text = "";
            responseText.text = "";
        }
    }
}
