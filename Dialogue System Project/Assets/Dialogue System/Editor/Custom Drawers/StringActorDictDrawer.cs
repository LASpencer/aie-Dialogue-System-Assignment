using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogue {
    [CustomPropertyDrawer(typeof(StringActorDict))]
    public class StringActorDictDrawer : SerializedDictionaryDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }
    }
}
