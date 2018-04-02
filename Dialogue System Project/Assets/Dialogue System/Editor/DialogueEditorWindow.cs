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

        List<EditorNode> nodes;

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

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);
            if (GUI.changed)
            {
                Repaint();
            }
        }

        private void DrawNodePanel()
        {
            nodePanel = new Rect(0, 0, position.width * nodePanelWidth, position.height);
            GUILayout.BeginArea(nodePanel);
            DrawNodes();
            GUILayout.EndArea();
        }

        private void DrawNodes()
        {
            //TODO
            if(nodes != null)
            {
                foreach(EditorNode node in nodes)
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
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && resizeBar.Contains(e.mousePosition))
                    {
                        isResizing = true;
                    } else if (e.button == 1)
                    {

                        Debug.Log("RMB down");
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
                    // HACK add an EventType.Used to jump to for used event
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if(nodes != null)
            {
                // Going backwards so last drawn is selected first
                for(int i = nodes.Count - 1; i >= 0; --i)
                {
                    nodes[i].ProcessEvents(e);
                    // HACK if e.type = EventType.Used, break loop
                }
            }
        }

        private void ResizePanels(Event e)
        {
            nodePanelWidth = e.mousePosition.x / position.width;
            Repaint();
        }

        private void ProcessContextMenu(Vector2 mousePos)
        {

            Debug.Log("Process context menu");
            Debug.Log("Mousepos = " + mousePos.ToString() + "; nodePanel = " + nodePanel.ToString() + "; position = " + position.ToString());
            GenericMenu menu = new GenericMenu();
            // Set items based on area clicked
            if (nodePanel.Contains(mousePos))
            {

                Debug.Log("Mouse with nodePanel, making nodePanel menu");
                menu.AddItem(new GUIContent("Add dialogue entry"), false, () => OnClickAddNode(mousePos));
                // TODO check if right clicked on node, do its menu instead
            }
            menu.ShowAsContext();
        }

        private void OnClickAddNode(Vector2 pos)
        {
            // HACK will need to change once binding data to conversation entries
            if(nodes == null)
            {
                nodes = new List<EditorNode>();
            }

            nodes.Add(new EditorNode(pos, NODE_WIDTH, NODE_HEIGHT, nodeStyle));
        }
    }
}