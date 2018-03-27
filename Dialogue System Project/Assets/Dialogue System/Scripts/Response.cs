using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [System.Serializable]
    public class Response
    {
        public string Text;
        public DialogueEvent OnChosen; //HACK maybe make a collection?
        public Condition Prerequisite;
        public TransitionList transitions;

        public bool CheckPrerequisite(DialogueManager manager)
        {
            if(Prerequisite == null)
            {
                return true;
            } else
            {
                return Prerequisite.Evaluate(manager);
            }
        }
    }

}