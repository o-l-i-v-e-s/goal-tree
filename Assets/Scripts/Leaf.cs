using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material leafMaterial;
    GameObject gameManager;
    UiManager uiManager;
    SaveManager saveManager;
    Material originalMaterial;
    MeshRenderer meshRenderer;
    string completedOn = "";
    Color[] colors = {
        /*// green 1 from palette
        new Color32(0, 67, 32, 255),
        // green 2 from palette
        new Color32(41, 127, 0, 255), new Color32(41, 127, 0, 255), new Color32(41, 127, 0, 255), new Color32(41, 127, 0, 255),
        // green 3 from palette
        new Color32(130, 211, 34, 255), new Color32(130, 211, 34, 255),
        // green 4 from palette
        new Color32(188, 245, 28, 255), new Color32(188, 245, 28, 255),
        // green 5 from palette
        new Color32(230, 243, 133, 255)*/
        new Color32(8, 97, 43, 255),
        new Color32(0, 115, 45, 255),
        new Color32(2, 143, 57, 255)
    };
    Color color;
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            uiManager = gameManager.GetComponentInChildren<UiManager>();
            if(uiManager == null)
            {
                Debug.LogError("uiManager is null");
            }
            saveManager = gameManager.GetComponentInChildren<SaveManager>();
            if (saveManager == null)
            {
                Debug.LogError("saveManager is null");
            }
        } else
        {
            Debug.LogError("gameManager is null on Leaf");
        }
    }

    private void OnMouseEnter()
    {
        if(completedOn == "")
        {
            meshRenderer.material = highlightMaterial;
            Cursor.SetCursor(CursorPointer, new Vector2(175,0), CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (completedOn == "")
        {
            meshRenderer.material = originalMaterial;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void HandleClick()
    {
        if(completedOn != "")
        {
            // If the leaf has already been completed, show information or a different modal
            Debug.Log("This leaf has already been completed on: " + completedOn);
        }
        else
        {
            // If the leaf has never been clicked, show a confirmation modal (Are you sure you want to mark this leaf as completed?)
            saveManager.LoadData();
            uiManager.OpenLeafModal(gameObject);
        }
    }

    public void HandleCompleteLeaf()
    {
        completedOn = Time.time.ToString();
        UpdateToRandomColor();
        saveManager.SaveData();
    }

    private void UpdateToRandomColor()
    {
        color = colors[Random.Range(0, colors.Length)];
        if (color != null)
        {
            meshRenderer.material = leafMaterial;
            meshRenderer.material.color = color;
        }
    }
}
