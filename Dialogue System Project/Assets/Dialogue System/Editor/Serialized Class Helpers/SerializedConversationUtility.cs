using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    class SerializedConversationUtility
    {
        public static SerializedProperty AddEntry(SerializedObject serialConversation)
        {
            SerializedProperty dialogueEntry = null;
            // Check object is a conversation
            Conversation conversation = serialConversation.targetObject as Conversation;
            if(conversation != null)
            {
                SerializedProperty entries = serialConversation.FindProperty("Entries");
                SerializedProperty nextID = serialConversation.FindProperty("nextID");
                int oldSize = entries.arraySize;
                entries.InsertArrayElementAtIndex(oldSize);
                dialogueEntry = entries.GetArrayElementAtIndex(oldSize);
                SerializedDialogueEntryUtility.Initialize(dialogueEntry, conversation, nextID.intValue);
                nextID.intValue = nextID.intValue + 1;
            }
            return dialogueEntry;
        }
    }
}
