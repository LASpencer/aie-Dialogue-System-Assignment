using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    //TODO figure out whether to use scriptable objects, or to have some DialogueData asset holding serializable fields
    public class DialogueActor : MonoBehaviour
    {
        public string Name;

        public FieldManager fields;
    }
}
