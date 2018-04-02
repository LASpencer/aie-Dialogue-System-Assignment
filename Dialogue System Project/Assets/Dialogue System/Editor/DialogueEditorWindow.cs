using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// TODO implementing undo/redo

namespace Dialogue
{
    public class DialogueEditorWindow : EditorWindow
    {

        const float RESIZER_WIDTH = 10.0f;
        const float PANEL_MIN_WIDTH = 0.05f;
        const int NODE_BORDER = 12;
        const float NODE_WIDTH = 200.0f;
        const float NODE_HEIGHT = 50.0f;

        Conversation conversation;

        List<EditorNode> nodes;

        EditorNode selectedNode;

        Rect nodePanel;
        Rect editPanel;
        Rect resizeBar;

        GUIStyle resizerStyle;
        GUIStyle nodeStyle;
        GUIStyle selectedNodeStyle;
        // TODO will need to have styles for different types of node? 
        
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

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(NODE_BORDER, NODE_BORDER, NODE_BORDER, NODE_BORDER);

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
            nodePanel = new Rect(0, 0, position.width * nodePanelWidth, position.height);
            GUILayout.BeginArea(nodePanel);
            // TODO draw grid
            DrawNodes();
            GUILayout.EndArea();
        }

        private void DrawNodes()
        {
            //TODO drawing connections
            if(nodes != null)
            {
                foreach(EditorNode node in nodes)
                {
                    node.Draw(node == selectedNode);
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

            // HACK maybe don't add this rect if we're dragging a node?
            EditorGUIUtility.AddCursorRect(resizeBar, MouseCursor.ResizeHorizontal);
        }

        private void ProcessEvents(Event e)
        {
            ProcessResizeBarEvents(e);
            ProcessEditPanelEvents(e);
            ProcessNodeEvents(e);
            ProcessNodePanelEvents(e);
        }

        private void ProcessResizeBarEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0 && resizeBar.Contains(e.mousePosition))
                    {
                        isResizing = true;
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isResizing = false;
                    break;

                case EventType.MouseDrag:
                    if (isResizing)
                    {
                        ResizePanels(e);
                        e.Use();
                    }
                    break;
                // Jump here if already used
                case EventType.Used:
                default:
                    break;
            }
        }

        private void ProcessEditPanelEvents(Event e)
        {
            //TODO edit panel uses event
        }

        private void ProcessNodePanelEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (nodePanel.Contains(e.mousePosition))
                    {
                        switch (e.button)
                        {
                            case 0: //LMB
                                // Deselect node
                                SelectNode(null);
                                e.Use();
                                break;
                            case 1: //RMB
                                ProcessContextMenu(e.mousePosition);
                                e.Use();
                                break;
                            case 2: //Middle
                                // TODO start dragging canvas
                                //TODO drag applies offset to grid
                                break;
                        }
                        
                    }
                    break;
                case EventType.MouseUp:
                    switch (e.button)
                    {
                        case 0: //LMB
                        case 1: //RMB
                            // Don't think these matter to it
                            break;
                        case 2:
                            //TODO stop dragging canvas
                            break;
                    }
                    break;
                case EventType.MouseDrag:
                    // TODO if dragging with button 2, drag each node with negative delta
                    break;
                case EventType.Used:
                default:
                    break;
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if(nodes != null)
            {
                // Going backwards so last drawn is selected first
                for(int i = nodes.Count - 1; i >= 0; --i)
                {
                    nodes[i].ProcessEvents(e, this);

                    // If the node's consumed the event, break loop
                    if (e.type == EventType.Used)
                    {
                        //TODO clicking on the node selects it, if node was selected move to end of list after iterating
                        break;
                    }
                }
            }
        }

        private void ResizePanels(Event e)
        {
            nodePanelWidth = Mathf.Clamp(e.mousePosition.x / position.width, PANEL_MIN_WIDTH, 1.0f - PANEL_MIN_WIDTH);
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

            EditorNode newNode = new EditorNode(pos, NODE_WIDTH, NODE_HEIGHT, nodeStyle, selectedNodeStyle);
            newNode.OnRemove = OnRemoveNode;
            
            // TODO add to coversation

            nodes.Add(newNode);
        }

        public void SelectNode(EditorNode node)
        {
            selectedNode = node;
            //TODO deselect connection if one selected
        }

        private void OnRemoveNode(EditorNode node)
        {
            //TODO save current state to allow undoing?

            // TODO find all incoming connections to node and delete them
            // TODO remove from conversation
            nodes.Remove(node);
        }
    }
}