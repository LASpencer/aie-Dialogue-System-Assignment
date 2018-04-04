using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomPropertyDrawer(typeof(TransitionList))]
    class TransitionListDrawer : PropertyDrawer
    {
        enum ListActions
        {
            Nothing,
            MoveUp,
            MoveDown,
            Delete
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //TODO add buttons for moving transitions, add/remove
            SerializedProperty optionsList = property.FindPropertyRelative("transitions");
            SerializedProperty defaultTransition = property.FindPropertyRelative("defaultTransition");
            int optionSize = optionsList.arraySize;

            ListActions action = ListActions.Nothing;
            int selectedIndex = -1;

            SerializedProperty option;
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            for (int i = 0; i < optionSize; ++i)
            {
                option = optionsList.GetArrayElementAtIndex(i);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(option,true);
                bool moveUp = GUILayout.Button("^");    //TODO grey if i=0
                bool moveDown = GUILayout.Button("v");  //TODO grey if i+1 = optionSize
                bool remove = GUILayout.Button("X");
                //TODO button value causes move up or down
                //TODO limit button size
                if (moveUp)
                {
                    action = ListActions.MoveUp;
                    selectedIndex = i;
                } else if (moveDown)
                {
                    action = ListActions.MoveDown;
                    selectedIndex = i;
                } else if (remove)
                {
                    action = ListActions.Delete;
                    selectedIndex = i;
                }
                EditorGUILayout.EndHorizontal();
            }
            //TODO put buttons to expand list here
            EditorGUILayout.PropertyField(defaultTransition);
            EditorGUILayout.EndVertical();

            // Handle choice
            switch (action){
                case ListActions.MoveUp:
                    //TODO move selected index up
                    break;
                case ListActions.MoveDown:
                    //TODO move selected index down
                    break;
                case ListActions.Delete:
                    //TODO delete at selected index
                    break;
                case ListActions.Nothing:
                default:
                    break;
            }

            //base.OnGUI(position, property, label);
        }
    }
}
