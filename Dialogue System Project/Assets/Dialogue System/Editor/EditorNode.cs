using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{
    class EditorNode
    {
        public Rect rect;

        public string title;

        public GUIStyle style;

        //TODO figure out how best to display, link nodes with dialogue entries

        public bool isDragged;

        public EditorNode(Vector2 position, float width, float height, GUIStyle nodeStyle)
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
            bool consumed = false;
            // TODO clicking starts dragging , unclick stops, drag while dragging moves by delta
            switch (e.type)
            {
                case EventType.MouseDown:
                    if(e.button == 0)   //LMB
                    {
                        GUI.changed = true;// HACK not sure we need this?
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            //consumed = true //HACK I think we may need this?
                        }
                    }
                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    break;
                case EventType.MouseDrag:
                    if(e.button == 0 && isDragged)  // LMB
                    {
                        Drag(e.delta);
                        e.Use();
                        consumed = true;
                        GUI.changed = true;
                    }
                    break;
                default:
                    break;
            }
            // Returns whether event consumed by node
            return consumed;
        }

    }
}
