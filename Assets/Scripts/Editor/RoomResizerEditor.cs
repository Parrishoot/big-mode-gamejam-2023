using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[CustomEditor(typeof(RoomResizer))]
public class RoomResizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RoomResizer resizer = (RoomResizer) target;

        DrawDefaultInspector();

        if(GUILayout.Button("Resize"))
        {
            resizer.Resize();
        }
    }
}
