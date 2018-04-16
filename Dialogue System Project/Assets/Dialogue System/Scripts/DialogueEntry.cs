using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dialogue { 

    [System.Serializable]
    public class DialogueEntry  {
        public int ID;          //HACK needs some system to detect/resolve collisions
        public string Title;
        [SerializeField]
        private int speakerIndex;

        public int SpeakerIndex { get { return speakerIndex; } set { speakerIndex = value; } }
        public string Speaker { get {
                if(speakerIndex >= 0 && speakerIndex < parent.Speakers.Count)
                {
                    return parent.Speakers[speakerIndex];
                } else
                {
                    return "";
                }
            } }

        [TextArea]
        public string Text;     // HACK Replace with LocalizedString when created
        public TransitionList transitions;
        public bool isEnd;
        public Conversation parent;

        [Header("Responses")]
        public List<Response> Responses;

        [HideInInspector]
        public Vector2 position; // Used to place node in editor window

        public DialogueEntry(Conversation parent, int id)
        {
            this.parent = parent;
            this.ID = id;
            transitions = new TransitionList();
        }

        /// <summary>
        /// Returns a name for the 
        /// </summary>
        /// <param name="maxChars">If greater than zero, truncate if more than this many characters</param>
        /// <param name="truncator">Appended to show name is truncated</param>
        /// <returns></returns>
        public string Name(bool prependID = false)
        {
            string name;
            bool usedID = false;
            if(!(string.IsNullOrEmpty(Title)))
            {
                name = Title;
            } else if (!string.IsNullOrEmpty(Text))
            {
                name = "\"" + Text + "\"";
            } else
            {
                name = "ID #" + ID.ToString();
                usedID = true;
            }
            if(prependID && !usedID)
            {
                name = "ID #" + ID.ToString() + ": " + name;
            }
            return name;
        }
	}
}
