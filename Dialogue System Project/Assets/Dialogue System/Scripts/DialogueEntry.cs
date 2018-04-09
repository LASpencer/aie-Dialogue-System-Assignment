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

        [HideInInspector]
        public Vector3 position; // Used to place node in editor window

        public DialogueEntry(Conversation parent, int id)
        {
            this.parent = parent;
            this.ID = id;
        }

        /// <summary>
        /// Returns a name for the 
        /// </summary>
        /// <param name="maxChars">If greater than zero, truncate if more than this many characters</param>
        /// <param name="truncator">Appended to show name is truncated</param>
        /// <returns></returns>
        public string Name(int maxChars = 0, string truncator = "...")
        {
            string name;
            bool quotes = false;
            if(!(string.IsNullOrEmpty(Title)))
            {
                name = Title;
            } else if (!string.IsNullOrEmpty(Text))
            {
                name = Text;
                quotes = true;
            } else
            {
                name = "ID #" + ID.ToString();
            }
            if(maxChars > 0 && name.Length > maxChars)
            {
                name = name.Substring(0, maxChars) + truncator;
                if (quotes)
                {
                    name = "\"" + name + "\"";
                }
            }
            return name;
        }
	}
}
