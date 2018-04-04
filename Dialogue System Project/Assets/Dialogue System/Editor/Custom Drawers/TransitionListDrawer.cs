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
        const float MOVE_BUTTON_WIDTH = 20;
        
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

            //TODO figure out good place to extract out this list drawing code (have attribute to use?)
            SerializedProperty option;
            EditorGUILayout.BeginVertical();
            //EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            optionsList.isExpanded = EditorGUILayout.Foldout(optionsList.isExpanded, label,true, EditorStyles.boldLabel);   //TODO create "Bold Foldout style"
            if (optionsList.isExpanded)
            {
                for (int i = 0; i < optionSize; ++i)
                {
                    option = optionsList.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(option, true); //TODO come up with better name than "Element i" to pass as label
                    bool moveUp = GUILayout.Button("^", GUILayout.Width(MOVE_BUTTON_WIDTH));    //TODO grey if i=0
                    bool moveDown = GUILayout.Button("v", GUILayout.Width(MOVE_BUTTON_WIDTH));  //TODO grey if i+1 = optionSize
                                                                                                //bool remove = GUILayout.Button("X");
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Separator();
                    //TODO button value causes move up or down
                    if (moveUp)
                    {
                        action = ListActions.MoveUp;
                        selectedIndex = i;
                    }
                    else if (moveDown)
                    {
                        action = ListActions.MoveDown;
                        selectedIndex = i;
                    }/* else if (remove)
                //{
                //    action = ListActions.Delete;
                //    selectedIndex = i;
                }*/

                }
                //TODO put buttons to expand list here
                GUILayout.BeginHorizontal();
                bool addItem = GUILayout.Button("Add Transition");
                bool clearItems = GUILayout.Button("Clear Transtions");
                GUILayout.EndHorizontal();
                if (addItem)
                {
                    optionsList.InsertArrayElementAtIndex(optionSize);
                }
                if (clearItems)
                {
                    optionsList.arraySize = 0;
                }

                // Handle choice
                switch (action)
                {
                    case ListActions.MoveUp:
                        if (selectedIndex > 0)
                        {
                            optionsList.MoveArrayElement(selectedIndex, selectedIndex - 1);
                        }
                        break;
                    case ListActions.MoveDown:
                        if (selectedIndex >= 0 && selectedIndex < (optionSize - 1))
                        {
                            optionsList.MoveArrayElement(selectedIndex, selectedIndex + 1);
                        }
                        break;
                    case ListActions.Delete:
                        //TODO delete at selected index
                        break;
                    case ListActions.Nothing:
                    default:
                        break;
                }

                EditorGUILayout.PropertyField(defaultTransition, true);
            } else
            {
                optionsList.isExpanded = GUILayout.Button("Show");
            }
            EditorGUILayout.EndVertical();

            

            //base.OnGUI(position, property, label);
        }
    }

    //[CustomPropertyDrawer(typeof(TransitionOption))]
    //class TransitionOptionDrawer : PropertyDrawer
    //{
    //    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //    {
    //        return base.GetPropertyHeight(property, label);
    //    }
    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //    {
    //        //base.OnGUI(position, property, label);
    //        SerializedProperty transition = property.FindPropertyRelative("transition");
    //        SerializedProperty condition = property.FindPropertyRelative("condition");
    //        EditorGUILayout.BeginVertical();
    //        EditorGUILayout.PropertyField(condition, true);
    //        EditorGUILayout.PropertyField(transition, true);
    //        EditorGUILayout.EndVertical();
    //    }
    //}


}
