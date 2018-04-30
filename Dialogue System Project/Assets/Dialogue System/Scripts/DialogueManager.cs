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
        [HideInInspector] //HACK figure out how to display normally
        public DialogueEntry current;

        //TODO have some way to save fields between scenes?
        FieldManager fields;
        //TODO figure out proper way to access name->actor map
        public Dictionary<string, DialogueActor> actors;

        private void Awake()
        {
            fields = new FieldManager();
            actors = new Dictionary<string, DialogueActor>();
            UISystem.manager = this;
        }

        private void Reset()
        {
            fields = new FieldManager();
            actors = new Dictionary<string, DialogueActor>();
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartConversation()
        {
            // TODO will need to be told which conversation to start
            current = conversation.Start;
            UISystem.OnConversationStart();
        }

        public void NextEntry()
        {
            bool entryFound = false;
            if (current != null)
            {
                // HACK 
                if (!current.isEnd)
                {
                    Transition selectedTransition = current.transitions.SelectTransition(this);
                    if(selectedTransition != null){
                        DialogueEntry nextEntry = conversation.FindEntry(selectedTransition.TargetID);
                        if(nextEntry != null)
                        {
                            current = nextEntry;
                            UISystem.SetDialogueEntry(current);
                            entryFound = true;
                        }
                    }
                    
                }
                if (!entryFound)
                {
                    EndConversation();
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
            if (id >= 0 && id < current.Responses.Count)
            {
                Response response = current.Responses[id];
                if (response.CheckPrerequisite(this))
                {
                    DialogueEvent e = response.OnChosen;
                    if (e != null)
                    {
                        e.Execute(this);
                    }

                    Transition selectedTransition = response.transitions.SelectTransition(this);

                    current = conversation.FindEntry(selectedTransition.TargetID);
                    UISystem.SetDialogueEntry(current);
                }
            }
        }

        public DialogueActor GetCurrentActor()
        {
            if(current != null)
            {
                return actors[current.Speaker];
            } else
            {
                return null;
            }
        }

        public void AssignActor(string key, DialogueActor actor)
        {
            //TODO have some kind of serializable dictionary so it can be done in editor
            actors[key] = actor;
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
