using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] Material highlightMaterial;
    GameObject gameManager;
    UiManager uiManager;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        gameManager = GameObject.Find("GameManager");
        if(gameManager != null)
        {
            uiManager = gameManager.GetComponentInChildren<UiManager>();
            if(uiManager == null)
            {
                Debug.LogError("uiManager is null");
            }
        } else
        {
            Debug.LogError("gameManager is null on Leaf");
        }
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

    public void HandleClick()
    {
        // placeholder modal
        uiManager.OpenLeafModal();
        // If the leaf has already been clicked, show information or a different modal
        // If the leaf has never been clicked, show a confirmation modal (Are you sure you want to mark this leaf as completed?)
    }
}
