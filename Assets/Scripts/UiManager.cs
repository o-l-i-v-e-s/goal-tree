using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] GameObject LeafModal;
    [SerializeField] GameObject SettingsModal;
    [SerializeField] GameObject SceneUi;
    [SerializeField] TextMeshProUGUI saveDataText;

    GameObject SelectedLeaf;

    void Start()
    {
        if (SceneUi == null)
        {
            Debug.LogError("No LeafModal present on SceneUi");
        }
        if (LeafModal == null)
        {
            Debug.LogError("No LeafModal present on UiManager");
        }
        if (SettingsModal == null)
        {
            Debug.LogError("No SettingsModal present on UiManager");
        }
    }

    private void OpenModal(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
            SceneUi.SetActive(false);
            EndHover();
        }
    }

    public void OpenSettingsModal()
    {
        OpenModal(SettingsModal);
    }

    public void UpdateSettingsModalSaveText(string jsonSaveData)
    {
        saveDataText.text = "Saved data: " + jsonSaveData;
    }

    public void OpenLeafModal(GameObject Leaf)
    {
        OpenModal(LeafModal);
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
        SettingsModal.SetActive(false);
        LeafModal.SetActive(false);
        SceneUi.SetActive(true);
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
