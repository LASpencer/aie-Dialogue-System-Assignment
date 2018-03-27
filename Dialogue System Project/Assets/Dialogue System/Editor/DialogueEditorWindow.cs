using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{
    public class DialogueEditorWindow : EditorWindow
    {
        const float RESIZER_WIDTH = 10.0f;
        const int NODE_BORDER = 12;
        const float NODE_WIDTH = 200.0f;
        const float NODE_HEIGHT = 50.0f;

        Conversation conversation;

        List<DialogueEditNode> nodes;

        Rect nodePanel;
        Rect editPanel;
        Rect resizeBar;

        GUIStyle resizerStyle;
        GUIStyle nodeStyle;

        // TODO maybe use an enum of states instead?
        bool isResizing;

        // HACK maybe edit panel should have absolute width?
        float nodePanelWidth = 0.8f;

        [MenuItem("Window/Dialogue")]
        static void Init()
        {
            DialogueEditorWindow window = EditorWindow.GetWindow<DialogueEditorWindow>();
            window.Show();
        }

        private void OnEnable()
        {
            resizerStyle = new GUIStyle();
            resizerStyle.normal.background = EditorGUIUtility.Load("icons/d_AvatarBlendBackground.png") as Texture2D;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(NODE_BORDER, NODE_BORDER, NODE_BORDER, NODE_BORDER);

        }

        private void OnGUI()
        {
            DrawNodePanel();
            DrawEditPanel();
            DrawResizeBar();

            ProcessEvents(Event.current);
            if (GUI.changed)
            {
                Repaint();
            }
        }

        private void DrawNodePanel()
        {
            editPanel = new Rect(0, 0, position.width * nodePanelWidth, position.height);
            GUILayout.BeginArea(editPanel);
            DrawNodes();
            GUILayout.EndArea();
        }

        private void DrawNodes()
        {
            //TODO
            if(nodes != null)
            {
                foreach(DialogueEditNode node in nodes)
                {
                    node.Draw();
                }
            }
        }

        private void DrawEditPanel()
        {
            editPanel = new Rect((position.width * nodePanelWidth) + 5, 0, position.width * (1 - nodePanelWidth) - (RESIZER_WIDTH * 0.5f), position.height);
            GUILayout.BeginArea(editPanel);
            conversation = (Conversation)EditorGUILayout.ObjectField("Conversation", conversation, typeof(Conversation), false);
            GUILayout.EndArea();
        }

        private void DrawResizeBar()
        {
            resizeBar = new Rect((position.width * nodePanelWidth) - (0.5f * RESIZER_WIDTH), 0, RESIZER_WIDTH, position.height);

            GUILayout.BeginArea(new Rect(resizeBar.position + (Vector2.right * 0.5f * RESIZER_WIDTH), new Vector2(2, position.height)), resizerStyle);
            GUILayout.EndArea();

            EditorGUIUtility.AddCursorRect(resizeBar, MouseCursor.ResizeHorizontal);
        }

        private void ProcessEvents(Event e)
        {
            switch(e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && resizeBar.Contains(e.mousePosition))
                    {
                        isResizing = true;
                    } else if (e.button == 1)
                    {
                        // Dropdown menu on right click
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;

                case EventType.MouseUp:
                    isResizing = false;
                    break;

                case EventType.MouseDrag:
                    if (isResizing)
                    {
                        ResizePanels(e);
                    }
                    break;
            }
        }

        private void ResizePanels(Event e)
        {
            nodePanelWidth = e.mousePosition.x / position.width;
            Repaint();
        }

        private void ProcessContextMenu(Vector2 mousePos)
        {
            GenericMenu menu = new GenericMenu();
            // Set items based on area clicked
            if (nodePanel.Contains(mousePos))
            {
                menu.AddItem(new GUIContent("Add dialogue entry"), false, () => OnClickAddNode(mousePos));
                // TODO check if right clicked on node, do its menu instead
            }
        }

        private void OnClickAddNode(Vector2 pos)
        {
            // HACK will need to change once binding data to conversation entries
            if(nodes == null)
            {
                nodes = new List<DialogueEditNode>();
            }

            nodes.Add(new DialogueEditNode(pos, NODE_WIDTH, NODE_HEIGHT, nodeStyle));
        }
    }
}