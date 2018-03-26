using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Condition : ScriptableObject
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // TODO figure out what should be passed
        virtual public bool Evaluate()
        {

            return true;
        }
    }
}
