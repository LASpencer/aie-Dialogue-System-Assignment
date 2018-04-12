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

        public static SerializedProperty AddResponse(SerializedProperty entry)
        {
            SerializedProperty newResponse = null;
            SerializedProperty responses = entry.FindPropertyRelative("Responses");
            if (responses != null && responses.isArray)
            {
                int oldSize = responses.arraySize;
                responses.InsertArrayElementAtIndex(oldSize);
                newResponse = responses.GetArrayElementAtIndex(oldSize);
                //TODO extract to some SerializedResponseUtility
                newResponse.FindPropertyRelative("Text").stringValue = "";
                newResponse.FindPropertyRelative("OnChosen").objectReferenceValue = null;
                newResponse.FindPropertyRelative("Prerequisite").objectReferenceValue = null;
                SerializedProperty transitionList = newResponse.FindPropertyRelative("transitions");
                if (transitionList.isArray)
                {
                    transitionList.ClearArray();
                }
                Vector2 entryPosition = entry.FindPropertyRelative("position").vector2Value;
                // HACK figure out best rule for initial position
                // position based on index rule
                //newResponse.FindPropertyRelative("Position").vector2Value = entryPosition + new Vector2(150, oldSize * 75);
                // below last in list rule
                if (oldSize == 0)
                {
                    newResponse.FindPropertyRelative("Position").vector2Value = entryPosition + new Vector2(250,0);
                }
                else
                {
                    newResponse.FindPropertyRelative("Position").vector2Value += new Vector2(0, 75);
                }
            }

            return newResponse;
        }
    }
}
