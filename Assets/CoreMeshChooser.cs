using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMeshChooser : MonoBehaviour
{
    [SerializeField]
    private List<Mesh> meshes;

    [SerializeField]
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter.mesh = GameUtil.GetRandomValueFromList(meshes);
    }
}
