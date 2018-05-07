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
        public List<DialogueEventInstance> OnChosen;
        public Condition Prerequisite;
        public TransitionList transitions;

        [HideInInspector]
        public Vector2 Position;// For display in editor

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