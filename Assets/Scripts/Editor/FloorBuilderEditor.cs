using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[CustomEditor(typeof(FloorBuilder))]
public class FloorBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FloorBuilder floorBuilder = (FloorBuilder) target;

        DrawDefaultInspector();

        if(GUILayout.Button("Build Floor"))
        {
            floorBuilder.CreateFloor();

            foreach(RoomResizer resizer in floorBuilder.GetRoomResizers()) {
                EditorUtility.SetDirty(resizer.GetEnemySpawner().GetComponent<Spawner>());
            }
        }
    }
}
