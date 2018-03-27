using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{
    class DialogueEditNode
    {
        public Rect rect;

        public string title;

        public GUIStyle style;

        //TODO figure out how best to display, link nodes with dialogue entries

        public bool isDragged;

        public DialogueEditNode(Vector2 position, float width, float height, GUIStyle nodeStyle)
        {
            rect = new Rect(position.x, position.y, width, height);
            style = nodeStyle;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw()
        {
            GUI.Box(rect, title, style);
        }

        public bool ProcessEvents(Event e)
        {
            // TODO clicking starts dragging , unclick stops, drag while dragging moves by delta
            return false;
        }

    }
}
