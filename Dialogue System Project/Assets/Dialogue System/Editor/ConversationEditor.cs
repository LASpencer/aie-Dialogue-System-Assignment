using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomEditor(typeof(Conversation))]
    class ConversationEditor : Editor
    {
        private int entrySelectedIndex;
        //private List<DialogueEntry> entries;
        SerializedProperty entries;
        SerializedProperty selectedEntry;


        private void OnEnable()
        {
            entries = serializedObject.FindProperty("Entries");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            selectedEntry = entries.GetArrayElementAtIndex(entrySelectedIndex);
            //entries = (target as Conversation).Entries;
            entrySelectedIndex = DialogueEntryPopup();
            EditorGUILayout.PropertyField(selectedEntry, true);
            // Do I need to use SerializedProperty/ApplyModifiedProperties?
            serializedObject.ApplyModifiedProperties();
            //base.OnInspectorGUI();
        }

        private int DialogueEntryPopup()
        {
            List<String> entryNames = new List<string>();
            foreach(DialogueEntry entry in ((target as Conversation).Entries))
            {
                entryNames.Add(entry.Name());
            }
            return EditorGUILayout.Popup(entrySelectedIndex, entryNames.ToArray());
        }
    }
}
