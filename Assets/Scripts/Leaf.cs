using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] Material highlightMaterial;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    private void OnMouseEnter()
    {
        meshRenderer.material = highlightMaterial;
        Cursor.SetCursor(CursorPointer, new Vector2(175,0), CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        meshRenderer.material = originalMaterial;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
