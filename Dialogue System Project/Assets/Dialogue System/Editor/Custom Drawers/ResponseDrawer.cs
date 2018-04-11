﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [CustomPropertyDrawer(typeof(Response), true)]
    class ResponseDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //TODO fix this up so it can appear both in editor and the custom window
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Text"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("OnChosen"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Prerequisite"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("transitions"));
            EditorGUILayout.EndVertical();
        }
    }
}
