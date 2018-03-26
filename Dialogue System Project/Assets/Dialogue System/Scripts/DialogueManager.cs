using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public DialogueUI UISystem;
        //HACK will later have multiple conversations to choose from
        public Conversation conversation;
        public DialogueEntry current;

        //TODO make more flexible system for displaying dialogue
        public Text dialogueText;
        public Text actorName;
        public Text responseText;

        // Use this for initialization
        void Start()
        {
            UISystem.manager = this;
            StartConversation();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void StartConversation()
        {
            // TODO will need to be told which conversation to start
            current = conversation.Entries[0];
            UISystem.OnConversationStart();
        }

        public void NextEntry()
        {
            if(current != null)
            {
                // HACK 
                if(current.isEnd)
                {
                    EndConversation();
                }
                else
                {
                    current = conversation.Entries[current.Next];
                    UISystem.SetDialogueEntry(current);
                }
            }
        }

        public void EndConversation()
        {
            current = null;
            UISystem.OnConversationEnd();
        }

        public void ResponseSelected(int id)
        {
            if(id >= 0 && id < current.Responses.Count)
            {
                current = conversation.Entries[current.Responses[id].selectedEntry];
                UISystem.SetDialogueEntry(current);
            }
        }
    }
}
