using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Event/Set Flag")]
    public class SetFlagEvent : DialogueEvent
    {
        public string flag;

        public override void Execute(DialogueManager manager)
        {
            manager.SetFlag(flag);
        }
    }
}
