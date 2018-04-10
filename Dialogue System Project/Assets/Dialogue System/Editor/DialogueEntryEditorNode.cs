using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    class DialogueEntryEditorNode : EditorNode
    {
        public DialogueEntry entry;
        public int entryID;
        public Conversation conversation;

        const int MAX_TITLE_CHARACTERS = 20;

        public DialogueEntryEditorNode(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle) : base(position, width, height, nodeStyle, selectedStyle)
        {
            
        }

        public override void Draw(bool selected)
        {
            title = StringUtility.TruncateString(entry.Name(), MAX_TITLE_CHARACTERS);//HACK may belong in OnGUI?
            base.Draw(selected);
            // Now a box is drawn, draw the rest
        }

        public override void Drag(Vector2 delta)
        {
            base.Drag(delta);
            entry.position = rect.position; //HACK move to OnGUI
        }
    }
}
