using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    class SerializedDialogueEntryUtility
    {
        public static void Initialize(SerializedProperty entry, Conversation parent, int ID)
        {
            SerializedProperty transitions = entry.FindPropertyRelative("transitions");
            SerializedProperty responses = entry.FindPropertyRelative("Responses");

            entry.FindPropertyRelative("parent").objectReferenceValue = parent;
            entry.FindPropertyRelative("ID").intValue = ID;
            entry.FindPropertyRelative("Title").stringValue = "";
            //TODO when Speaker is string, make empty string
            entry.FindPropertyRelative("Text").stringValue = "";
            entry.FindPropertyRelative("isEnd").boolValue = false;
            if (transitions.isArray) {
                transitions.ClearArray();
            }
            if (responses.isArray)
            {
                responses.ClearArray();
            }
            entry.FindPropertyRelative("position").vector2Value = Vector2.zero;
        }
    }
}
