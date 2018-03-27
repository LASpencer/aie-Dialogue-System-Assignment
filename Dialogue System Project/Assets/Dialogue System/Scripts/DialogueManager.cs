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

        //TODO have some way to save fields between scenes?
        FieldManager fields;

        private void Awake()
        {
            fields = new FieldManager();
        }

        private void Reset()
        {
            fields = new FieldManager();
        }

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
            // TODO maybe get entry titled START?
            current = conversation.Entries[0];
            UISystem.OnConversationStart();
        }

        public void NextEntry()
        {
            if (current != null)
            {
                // HACK 
                if (current.isEnd)
                {
                    EndConversation();
                }
                else
                {
                    Transition selectedTransition = current.transitions.SelectTransition(this);
                    current = conversation.FindEntry(selectedTransition.TargetID);
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
            //HACK might replace argument with response, and have UI manager know about responses?
            if (id >= 0 && id < current.Responses.Count)
            {
                DialogueEvent e = current.Responses[id].OnChosen;
                if(e != null)
                {
                    e.Execute(this);
                }

                Transition selectedTransition = current.Responses[id].transitions.SelectTransition(this);

                current = conversation.FindEntry(selectedTransition.TargetID);
                UISystem.SetDialogueEntry(current);
            }
        }

        public void SetFlag(string flag)
        {
            fields.SetFlag(flag);
        }

        public void UnsetFlag(string flag)
        {
            fields.UnsetFlag(flag);
        }

        public bool CheckFlag(string flag)
        {
            return fields.CheckFlag(flag);
        }
    }
}
