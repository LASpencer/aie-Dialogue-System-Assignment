using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class DialogueEventInstance
    {
        [SerializeField]
        string target;
        [SerializeField]
        string parameters;
        [SerializeField]
        DialogueEvent dialogueEvent;

        public void Execute(DialogueManager manager)
        {
            dialogueEvent.Execute(manager, target, parameters);
        }
    }
}
