using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Event/Set Flag")]
    public class SetFlagEvent : DialogueEvent
    {
        public string flag;

        public override void Execute(DialogueManager manager, string target, string parameters)
        {
            FieldManager fields;
            if (target == "")
            {
                fields = manager.fields;
            } else
            {
                try {
                    fields = manager.actors[target].fields;
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogWarning("Actor '" + target + "' not found in dialogue manager");
                    return;
                }
            }
            //TODO parse parameters as saying flags to set/unset
            fields.SetFlag(parameters);
        }
        
        public override string Describe(string target, string parameters)
        {
            string description = "Set flag '" + parameters + "'";
            if (target == "")
            {
                description += " on " + target;
            }
            return description;
        }
    }
}
