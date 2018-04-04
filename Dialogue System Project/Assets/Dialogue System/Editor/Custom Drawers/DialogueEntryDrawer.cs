using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomPropertyDrawer(typeof(DialogueEntry))]
    class DialogueEntryEditor : PropertyDrawer
    {
        const float ID_LABEL_HEIGHT = 15;
        const float ID_LABEL_WIDTH = 25;
        const float LINE_MARGIN = 2;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty id = property.FindPropertyRelative("ID");
            SerializedProperty title = property.FindPropertyRelative("Title");
            SerializedProperty speaker = property.FindPropertyRelative("Speaker");
            SerializedProperty isEnd = property.FindPropertyRelative("isEnd");
            SerializedProperty text = property.FindPropertyRelative("Text");
            SerializedProperty transitions = property.FindPropertyRelative("transitions");
            SerializedProperty responses = property.FindPropertyRelative("Responses");

            //EditorGUI.BeginProperty(position, label, property);

            //// Setup layout rects
            //Rect idLabelRect = new Rect(position.x, position.y, ID_LABEL_WIDTH, EditorGUI.GetPropertyHeight(id));
            //Rect idFieldRect = new Rect(position.x + ID_LABEL_WIDTH, position.y, position.width - ID_LABEL_WIDTH, EditorGUI.GetPropertyHeight(id));
            //Rect titleRect = new Rect(position.x, idFieldRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(title));
            //Rect speakerRect = new Rect(position.x, titleRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(speaker));
            //Rect isEndRect = new Rect(position.x, speakerRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(isEnd));
            //Rect textRect = new Rect(position.x, isEndRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(text));
            //// HACK may need to change when transition drawer made
            //Rect transitionsRect = new Rect(position.x, textRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(transitions, true));
            //// HACK may need to change when response drawer made
            //Rect responseRect = new Rect(position.x, transitionsRect.yMax + LINE_MARGIN, position.width, EditorGUI.GetPropertyHeight(responses, true));
            //// Show fields
            //EditorGUI.LabelField(idLabelRect, "ID: ");
            //EditorGUI.SelectableLabel(idFieldRect, id.intValue.ToString());
            //EditorGUI.PropertyField(titleRect, title);
            //EditorGUI.PropertyField(speakerRect, speaker);
            //EditorGUI.PropertyField(isEndRect, isEnd);
            //EditorGUI.PropertyField(textRect, text);
            //EditorGUI.PropertyField(transitionsRect, transitions,true);
            //EditorGUI.PropertyField(responseRect, responses,true);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID: ");
            EditorGUILayout.SelectableLabel(id.intValue.ToString());
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(title);
            EditorGUILayout.PropertyField(speaker);
            EditorGUILayout.PropertyField(isEnd);
            EditorGUILayout.PropertyField(text);
            EditorGUILayout.PropertyField(transitions,true);
            EditorGUILayout.PropertyField(responses,true);

            EditorGUILayout.EndVertical();

            //EditorGUI.EndProperty();
            //base.OnGUI(position, property, label);
        }

    }
}
