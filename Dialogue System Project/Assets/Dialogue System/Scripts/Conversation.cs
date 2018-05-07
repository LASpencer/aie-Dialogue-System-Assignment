using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName ="Dialogue/Conversation")]
    public class Conversation : ScriptableObject
    {
        [SerializeField][HideInInspector]
        int nextID = 0;

        public int NextID { get { return nextID; } }

        [SerializeField]
        private int startingID;

        //HACK maybe write a serializable dictionary?
        public List<DialogueEntry> Entries = new List<DialogueEntry>();

        public List<string> Speakers = new List<string>();

        public DialogueEntry Start { get { return FindEntry(startingID); } }

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

        public DialogueEntry AddEntry()
        {
            DialogueEntry newEntry = new DialogueEntry(this, nextID);
            if (Entries == null)
            {
                Entries = new List<DialogueEntry>();
            }
            ++nextID;
            Entries.Add(newEntry);
            return newEntry;
        }
        // TODO AddEntry to create a new entry with unique ID, add to end of list

        public void RemoveEntry(DialogueEntry entry)
        {
            Entries.Remove(entry);
        }


    }
}
