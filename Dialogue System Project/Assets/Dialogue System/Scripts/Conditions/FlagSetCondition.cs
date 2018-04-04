﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName ="Dialogue/Condition/Flag Set")]
    public class FlagSetCondition : Condition
    {
        public string Flag;

        public override bool Evaluate(DialogueManager dialogue)
        {
            return dialogue.CheckFlag(Flag) != Not;
        }

        override public string Describe()
        {
            return "'" + Flag + "' = " + (!Not).ToString();
        }
    }

}