using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string fileName = "save.txt";
    UiManager uiManager;

    private void Start()
    {
        uiManager = gameObject.GetComponent<UiManager>();
        if(uiManager == null)
        {
            Debug.LogError("uiManager is null on SaveManager");
        }
    }

    public class SavedData : MonoBehaviour
    {
        public string CurrentTime;
        public string Label;
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        bool doesFileExist = File.Exists(path);
        if(doesFileExist)
        {
            string loadedData = File.ReadAllText(path);
            Debug.Log("LOADED DATA: ");
            Debug.Log(loadedData);
            uiManager.UpdateSettingsModalSaveText(loadedData);
        }
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        SavedData savedData = new SavedData();
        // Data to save
        //  per-leaf data
        //    completed on
        //    color
        savedData.CurrentTime = Time.deltaTime.ToString();
        savedData.Label = "test string!";
        string jsonObject = JsonUtility.ToJson(savedData);
        Debug.Log("jsonObject: ");
        Debug.Log(jsonObject);
        Debug.Log(path);
        File.WriteAllText(path, jsonObject);
        uiManager.UpdateSettingsModalSaveText(jsonObject);
    }

    public void RandomizeSaveData()
    {
        Debug.Log("RandomizeSaveData");
    }
}
