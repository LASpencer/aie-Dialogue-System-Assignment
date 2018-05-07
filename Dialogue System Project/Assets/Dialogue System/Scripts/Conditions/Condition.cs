using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Condition : ScriptableObject
    {
        public bool Not;
        virtual public bool Evaluate(DialogueManager dialogue)
        {

            return true != Not;
        }

        virtual public string Describe()
        {
            return (!Not).ToString();
        }
    }
}
