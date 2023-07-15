using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] GameObject LeafModal;
    GameObject SelectedLeaf;

    void Start()
    {
        if(LeafModal == null)
        {
            Debug.LogError("No LeafModal present on UiManager");
        }
    }

    public void OpenLeafModal(GameObject Leaf)
    {
        Debug.Log("OpenLeafModal");
        LeafModal.SetActive(true);
        SelectedLeaf = Leaf;
    }

    public void ConfirmCompleteLeaf()
    {
        if(SelectedLeaf == null)
        {
            Debug.LogError("SelectedLeaf is null when completing Leaf on UiManager");
        } else
        {
            Leaf leaf = SelectedLeaf.GetComponent<Leaf>();
            leaf.HandleCompleteLeaf();
            CloseAllModals();
        }
    }

    public void CloseAllModals()
    {
        LeafModal.SetActive(false);
        SelectedLeaf = null;
        EndHover();
    }

    public void StartHover()
    {
        Cursor.SetCursor(CursorPointer, new Vector2(175, 0), CursorMode.Auto);
    }

    public void EndHover()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
