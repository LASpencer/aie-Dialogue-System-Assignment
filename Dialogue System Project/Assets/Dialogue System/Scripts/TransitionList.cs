﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public struct TransitionOption
    {
        public Transition transition;
        public Condition condition;
       
    }

    [System.Serializable]
    public class TransitionList
    {
        public List<TransitionOption> transitions;

        // Chosen if none in list succeed
        public Transition defaultTransition;

        public Transition SelectTransition(DialogueManager dialogue)
        {
            // Return first transition to succeed
            foreach(TransitionOption option in transitions)
            {
                if(option.condition == null || option.condition.Evaluate(dialogue))
                {
                    return option.transition;
                }
            }
            return defaultTransition;
        }
    }
}