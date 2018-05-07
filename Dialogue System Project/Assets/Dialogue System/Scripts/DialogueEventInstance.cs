using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class DialogueEventInstance : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string description;
#endif

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

        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            if(dialogueEvent != null)
            {
                description = dialogueEvent.Describe(target, parameters);
            }
#endif
        }

        public void OnBeforeSerialize()
        {

        }
    }
}
