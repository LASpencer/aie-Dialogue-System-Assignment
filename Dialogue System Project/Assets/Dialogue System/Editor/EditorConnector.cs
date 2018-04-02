using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue
{
    public class EditorConnector
    {
        EditorNode Parent;
        EditorNode Target;

        Vector2 start;
        Vector2 end;

        public void Draw()
        {
            //TODO use handles to draw line from start to end, with triangle in middle
            //TODO if target has connector to parent, offset start and end (relative to direction, so other line offset opposite way)
            // Selected connector is different colour
        }

        public void ProcessEvents(Event e, DialogueEditorWindow window)
        {
            //TODO clicking selects the connection
            // knows if it was clicked by checking mouse position min distance to line is small enough
            

        }

        private float DistanceToLine(Vector2 point)
        {
            Vector2 direction = (end - start).normalized;
            Vector2 mouseOffset = (point - start);
            float endPointDistance = Vector2.Dot(end - start, direction);
            float nearestPointDistance = Mathf.Clamp(Vector2.Dot(mouseOffset, direction), 0, endPointDistance);
            Vector2 nearestPoint = nearestPointDistance * direction;
            return (mouseOffset - nearestPoint).magnitude;
        }
    }
}
