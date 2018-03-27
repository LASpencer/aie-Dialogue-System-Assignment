using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Condition : ScriptableObject
    {
        // TODO figure out what should be passed
        virtual public bool Evaluate(DialogueManager dialogue)
        {

            return true;
        }
    }
}
