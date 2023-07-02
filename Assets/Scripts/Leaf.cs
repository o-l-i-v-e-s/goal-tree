using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void OnMouseEnter()
    {
        meshRenderer.material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        meshRenderer.material = originalMaterial;
    }
}
