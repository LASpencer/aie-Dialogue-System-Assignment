using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dialogue { 

    [System.Serializable]
    public class DialogueEntry  {
        public int ID;          //HACK needs some system to detect/resolve collisions
        public string Title;
        public Actor Speaker;   //HACK might change to string, with conversation holding map of names to actor object
        [TextArea]
        public string Text;     // HACK Replace with LocalizedString when created
        public TransitionList transitions;
        public bool isEnd;
        public Conversation parent;

        [Header("Responses")]
        public List<Response> Responses;

        public DialogueEntry(Conversation parent, int id)
        {
            this.parent = parent;
            this.ID = id;
        }

        public string Name()
        {
            if(!(string.IsNullOrEmpty(Title)))
            {
                return Title;
            } else if (!string.IsNullOrEmpty(Text))
            {
                return Text;
            } else
            {
                return "ID #" + ID.ToString();
            }
        }
	}
}
