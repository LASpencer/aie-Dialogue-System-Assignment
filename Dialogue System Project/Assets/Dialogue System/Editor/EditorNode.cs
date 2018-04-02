﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{
    public class EditorNode
    {
        public Rect rect;

        public string title;

        public GUIStyle Style;
        public GUIStyle SelectedStyle;

        public Action<EditorNode> OnRemove;

        //TODO figure out how best to display, link nodes with dialogue entries + responses

        public bool isDragged;

        public List<EditorConnector> Connections;

        public EditorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle)
        {
            rect = new Rect(position.x, position.y, width, height);
            Style = nodeStyle;
            SelectedStyle = selectedStyle;
            Connections = new List<EditorConnector>();
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw(bool selected)
        {
            if (selected)
            {
                GUI.Box(rect, title, SelectedStyle);
            }
            else
            {
                GUI.Box(rect, title, Style);
            }
        }

        public void ProcessEvents(Event e, DialogueEditorWindow window)
        {
            // TODO clicking starts dragging , unclick stops, drag while dragging moves by delta
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (rect.Contains(e.mousePosition))
                    {
                        switch (e.button)
                        {
                            case 0://LMB
                                GUI.changed = true;
                                isDragged = true;
                                window.SelectNode(this);
                                e.Use();
                                break;
                            case 1: //RMB
                                // Create dropdown menu
                                ProcessContextMenu();
                                e.Use();
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case EventType.MouseUp:
                    switch (e.button)
                    {
                        case 0: //LMB up
                            isDragged = false;
                            break;
                        default:
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    if(e.button == 0 && isDragged)  // LMB
                    {
                        Drag(e.delta);
                        e.Use();
                        GUI.changed = true;
                    }
                    break;
                default:
                    break;
            }
            // Returns whether event consumed by node
        }

        private void ProcessContextMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, OnClickDelete);
            menu.ShowAsContext();
        }

        private void OnClickDelete()
        {
            if(OnRemove != null)
            {
                OnRemove(this);
            }
        }
    }
}
