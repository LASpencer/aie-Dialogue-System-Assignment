using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomEditor(typeof(DialogueEntry))]
    class DialogueEntryEditor : Editor
    {
        SerializedProperty ID;
        SerializedProperty Title;
        SerializedProperty Speaker;
        SerializedProperty Text;
        SerializedProperty transitions;

        SerializedProperty isEnd;
        SerializedProperty Responses;

        public void OnEnable()
        {
            ID = serializedObject.FindProperty("ID");
            Title = serializedObject.FindProperty("Title");
            Speaker = serializedObject.FindProperty("Speaker");
            Text = serializedObject.FindProperty("Text");
            transitions = serializedObject.FindProperty("transitions");
            isEnd = serializedObject.FindProperty("isEnd");
            Responses = serializedObject.FindProperty("Responses");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField(new GUIContent("ID " + ID.ToString()));
            EditorGUILayout.PropertyField(Title);
            EditorGUILayout.PropertyField(Speaker);
            Text.stringValue = EditorGUILayout.TextArea(Text.stringValue);
            EditorGUILayout.PropertyField(isEnd);
            EditorGUILayout.PropertyField(transitions, true);
            EditorGUILayout.PropertyField(Responses, true);
            // TODO figure out nicer way to show transitions, 
            //TODO change how actor chosen? Dropdown populated by parent conversation?
            serializedObject.ApplyModifiedProperties();

            //base.OnInspectorGUI();
        }

    }
}
