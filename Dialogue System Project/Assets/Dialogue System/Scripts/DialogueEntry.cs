using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue { 

    [System.Serializable]
    public class DialogueEntry {
        public int ID;          //HACK needs some system to detect/resolve collisions
        public string Title;
        public Actor Speaker;
        public string Text;     // HACK Replace with LocalizedString when created
        public TransitionList transitions;
        public bool isEnd;

        [Header("Responses")]
        public List<Response> Responses;
	}
}
