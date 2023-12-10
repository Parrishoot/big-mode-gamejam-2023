using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    private Dictionary<MeshRenderer, Material> originalMaterials = new Dictionary<MeshRenderer, Material>();

    void Start()
    {
        foreach(MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>()) {
            originalMaterials.Add(meshRenderer, meshRenderer.material);
        }
    }

    public void SetAll(Material material) {
        foreach(MeshRenderer meshRenderer in originalMaterials.Keys) {
            meshRenderer.material = material;
        }
    }

    public void RevertAll() {
        foreach(MeshRenderer meshRenderer in originalMaterials.Keys) {
            meshRenderer.material = originalMaterials[meshRenderer];
        }
    }
}
