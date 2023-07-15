using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Texture2D CursorPointer;
    [SerializeField] GameObject LeafModal;

    void Start()
    {
        if(LeafModal == null)
        {
            Debug.LogError("No LeafModal present on UiManager");
        }
    }

    public void OpenLeafModal()
    {
        Debug.Log("OpenLeafModal");
        LeafModal.SetActive(true);
    }

    public void CloseAllModals()
    {
        LeafModal.SetActive(false);
        EndHover();
    }

    public void StartHover()
    {
        Debug.Log("HOVERING");
        Cursor.SetCursor(CursorPointer, new Vector2(175, 0), CursorMode.Auto);
    }

    public void EndHover()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
