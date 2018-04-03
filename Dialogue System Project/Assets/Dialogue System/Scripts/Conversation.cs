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

        public DialogueEntry Start;

        public DialogueEntry FindEntry(int id)
        {
            DialogueEntry entry = Entries.Find(e => e.ID == id);
            return entry;
        }

        public DialogueEntry FindEntry(string title)
        {
            DialogueEntry entry = Entries.Find(e => e.Title == title);
            return entry;
        }
    }
}
