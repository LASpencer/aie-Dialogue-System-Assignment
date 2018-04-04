using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue {
    [CustomPropertyDrawer(typeof(Condition), true)]
    class ConditionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 15;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect basePosition = new Rect(position.x, position.y, position.width, position.height - 15);
            Rect labelPosition = new Rect(position.x + EditorGUIUtility.labelWidth, basePosition.yMax, position.width - EditorGUIUtility.labelWidth, 15);
            string description = "Unconditional";
            Condition target = (Condition)property.objectReferenceValue;
            if (target != null)
            {
                description = target.Describe();
            }
            EditorGUI.BeginProperty(position, label, property);
            //base.OnGUI(position, property, label);
            EditorGUI.PropertyField(basePosition, property);
            EditorGUI.LabelField(labelPosition, description); //T
            EditorGUI.EndProperty();
        }
    }
}