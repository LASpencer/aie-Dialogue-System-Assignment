using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    public class DialogueEntryEditorNode : EditorNode
    {
        public DialogueEntry entry;
        public int entryID;
        public Conversation conversation;

        const int MAX_TITLE_CHARACTERS = 20;

        public DialogueEntryEditorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle) : base(position, width, height, nodeStyle, selectedStyle)
        {
            
        }

        public override void Draw(bool selected)
        {
            title = StringUtility.TruncateString(entry.Name(), MAX_TITLE_CHARACTERS);//HACK may belong in OnGUI?
            base.Draw(selected);
            // Now a box is drawn, draw the rest
        }

        public override void Drag(Vector2 delta)
        {
            base.Drag(delta);
            entry.position = rect.position; //HACK move to OnGUI
        }

        protected override void ProcessContextMenu(DialogueEditorWindow window)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Transition"), false, OnClickNewTransition);
            menu.AddItem(new GUIContent("Delete"), false, () => OnClickDelete(window));
            menu.ShowAsContext();
        }

        protected override void OnClickDelete(DialogueEditorWindow window)
        {
            SerializedObject conversation = window.SerializedConversation;
            if(conversation != null)
            {
                conversation.Update();
                // Find index in conversation
                SerializedProperty entries = conversation.FindProperty("Entries");
                int index = SerializedArrayUtility.FindIndexByValue(entries, "ID", entryID);
                if (index >= 0)
                {
                    entries.DeleteArrayElementAtIndex(index);
                }
                conversation.ApplyModifiedProperties();
            }
        }

        protected override void OnClickAsTarget(DialogueEditorWindow window, EditorConnector connector)
        {
            // DialogueEntry can be connected to, so ask connector's parent to try it and if so make connection
            if (connector.Parent.ConnectObjectToDialogueEntry(window, entryID))
            {
                connector.Parent.Connections.Add(connector);
                connector.Target = this;
            }
        }

        protected internal override bool ConnectObjectToDialogueEntry(DialogueEditorWindow window, int targetID)
        {
            Debug.Log("Attepting connection from entry #" + entryID.ToString() + " to #" + targetID.ToString());
            // Get Conversation object from window
            SerializedObject conversation = window.SerializedConversation;
            bool success = false;
            if(conversation != null)
            {
                Debug.Log("Conversation found");
                conversation.Update();
                Debug.Log("Updating Conversation Serialized Object");
                SerializedProperty entries = conversation.FindProperty("Entries");
                // Find own entry and target entry properties in conversation
                SerializedProperty serializedEntry = SerializedArrayUtility.FindPropertyByValue(entries, "ID", entryID);
                SerializedProperty serializedTarget = SerializedArrayUtility.FindPropertyByValue(entries, "ID", targetID);
                Debug.Log("Serialized Entry: " + serializedEntry.ToString());
                SerializedProperty transitions = serializedEntry.FindPropertyRelative("transitions.transitions");
                Debug.Log("list of transition options: " + transitions.ToString());
                int oldSize = transitions.arraySize;
                transitions.InsertArrayElementAtIndex(oldSize);
                SerializedProperty newTransition = transitions.GetArrayElementAtIndex(oldSize);
                Debug.Log("Newly added transition option: " + newTransition.ToString());
                newTransition.FindPropertyRelative("condition").objectReferenceValue = null;
                Debug.Log("Set transition option's condition to null");
                newTransition.FindPropertyRelative("transition.TargetID").intValue = targetID;
                Debug.Log("Set transition option's targetID to " + targetID.ToString());
                Debug.Log("Transition option after: " + newTransition.ToString());

                conversation.ApplyModifiedProperties();
                Debug.Log("Conversation modified.");
                success = true;
            }
            return success;
        }
    }
}
