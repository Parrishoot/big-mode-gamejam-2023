using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    private Dictionary<MeshRenderer, Material> originalMaterials = new Dictionary<MeshRenderer, Material>();

    void Awake()
    {
        foreach(MeshRenderer otherMeshRenderer in GetComponentsInChildren<MeshRenderer>()) {
            originalMaterials.Add(otherMeshRenderer, otherMeshRenderer.material);
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

    public void SetShaderFloatValue(String shaderPropertyName, float value) {
        foreach(MeshRenderer meshRenderer in originalMaterials.Keys) {
            meshRenderer.material.SetFloat(shaderPropertyName, value);
        }
    }

    public void SetAllActive() {
        foreach(MeshRenderer meshRenderer in originalMaterials.Keys) {
            meshRenderer.gameObject.SetActive(true);
        }
    }

    public void SetAllInActive() {
        foreach(MeshRenderer meshRenderer in originalMaterials.Keys) {
            meshRenderer.gameObject.SetActive(false);
        }
    }
}
