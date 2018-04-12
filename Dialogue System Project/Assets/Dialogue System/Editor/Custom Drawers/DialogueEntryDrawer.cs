using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    enum ListActions
    {
        Nothing,
        MoveUp,
        MoveDown,
        Delete
    }

    [CustomPropertyDrawer(typeof(DialogueEntry))]
    class DialogueEntryEditor : PropertyDrawer
    {
        const float ID_LABEL_HEIGHT = 15;
        const float ID_LABEL_WIDTH = 25;
        const float LINE_MARGIN = 2;
        const float MOVE_BUTTON_WIDTH = 20;

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
            SerializedProperty responsesList = property.FindPropertyRelative("Responses");
            SerializedProperty response;

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
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(transitions,true);

            EditorGUILayout.Separator();

            // Responses list area
            int responsesSize = responsesList.arraySize;
            ListActions action = ListActions.Nothing;
            int selectedIndex = -1;
            //EditorGUILayout.LabelField("Responses", EditorStyles.boldLabel);
            responsesList.isExpanded = EditorGUILayout.Foldout(responsesList.isExpanded, "Responses", true, EditorStyles.boldLabel);
            if (responsesList.isExpanded)
            {
                for (int i = 0; i < responsesSize; ++i)
                {
                    response = responsesList.GetArrayElementAtIndex(i);

                    string responseTitle = response.FindPropertyRelative("Text").stringValue;
                    if (string.IsNullOrEmpty(responseTitle))
                    {
                        responseTitle = "Response " + i.ToString();
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    response.isExpanded = EditorGUILayout.Foldout(response.isExpanded, responseTitle);
                    if (response.isExpanded)
                    {
                        EditorGUILayout.PropertyField(response, new GUIContent(responseTitle), true); //TODO get text from response, use as label
                    }
                    EditorGUILayout.EndVertical();
                    bool moveUp = false, moveDown = false, deleteResponse = false;
                    using (new EditorGUI.DisabledScope(i <= 0))     // Disable if first in array
                    {
                        moveUp = GUILayout.Button("^", GUILayout.Width(MOVE_BUTTON_WIDTH));
                    }
                    using (new EditorGUI.DisabledScope(i + 1 >= responsesSize))
                    {
                        moveDown = GUILayout.Button("v", GUILayout.Width(MOVE_BUTTON_WIDTH));
                    }
                    deleteResponse = GUILayout.Button("X", GUILayout.Width(MOVE_BUTTON_WIDTH));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Separator();

                    if (moveUp)
                    {
                        action = ListActions.MoveUp;
                        selectedIndex = i;
                    }
                    else if (moveDown)
                    {
                        action = ListActions.MoveDown;
                        selectedIndex = i;
                    } else if (deleteResponse)
                    {
                        action = ListActions.Delete;
                        selectedIndex = i;
                    }
                }
                //TODO put buttons to expand list here
                GUILayout.BeginHorizontal();
                bool addItem = GUILayout.Button("Add Response");
                bool clearItems = GUILayout.Button("Clear Responses");
                GUILayout.EndHorizontal();
                if (addItem)
                {
                    responsesList.InsertArrayElementAtIndex(responsesSize);
                }
                if (clearItems)
                {
                    responsesList.arraySize = 0;
                }
                switch (action)
                {
                    case ListActions.MoveUp:
                        if (selectedIndex > 0)
                        {
                            responsesList.MoveArrayElement(selectedIndex, selectedIndex - 1);
                        }
                        break;
                    case ListActions.MoveDown:
                        if (selectedIndex >= 0 && selectedIndex < (responsesSize - 1))
                        {
                            responsesList.MoveArrayElement(selectedIndex, selectedIndex + 1);
                        }
                        break;
                    case ListActions.Delete:
                        //TODO delete at selected index
                        if(selectedIndex >= 0 && selectedIndex < (responsesSize))
                        {
                            responsesList.DeleteArrayElementAtIndex(selectedIndex);
                        }
                        break;
                    case ListActions.Nothing:
                    default:
                        break;
                }
            }
            else
            {
                responsesList.isExpanded = GUILayout.Button("Show");
            }

            EditorGUILayout.EndVertical();

            //EditorGUI.EndProperty();
            //base.OnGUI(position, property, label);
        }

    }
}
