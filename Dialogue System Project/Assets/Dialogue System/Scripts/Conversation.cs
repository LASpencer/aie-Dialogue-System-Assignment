using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName ="Dialogue/Conversation")]
    public class Conversation : ScriptableObject
    {
        //HACK maybe write a serializable dictionary?
        public List<DialogueEntry> Entries;
    }
}
