using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml.Serialization;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private void Awake()
    {
        SetupInstance();
    }

    public void SetupInstance()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            gameObject.name = "SaveSystem";
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    [Header("Save Data")]
    public SaveData saveData;

    [Header("Build Settings")]
    public string dataPath;
    public enum dataPathSwitch
    {
        Vita,
        PC,
    }

    public dataPathSwitch buildTarget;

    private void Start()
    {
        GetSavePath();

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
    }

    private string GetSavePath()
    {
        switch (buildTarget)
        {
            case dataPathSwitch.Vita:
                return "ux0:data/vitaReckoning";
            case dataPathSwitch.PC:
                return Application.persistentDataPath;
            default:
                return string.Empty;
        }
    }

    public void Save()
    {
        Debug.Log("Saving data");

        dataPath = GetSavePath();

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/xd.sav", FileMode.Create);

        serializer.Serialize(stream, saveData);
        stream.Close();
    }

    public void Load()
    {
        dataPath = GetSavePath();

        if (File.Exists(dataPath + "/xd.sav"))
        {
            Debug.Log("Loading data");

            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/xd.sav", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            Debug.LogWarning("Couldn't find data to load!");
        }
    }
    public void ClearSave()
    {
        dataPath = GetSavePath();

        File.Delete(dataPath + "/xd.sav");
        SaveSystemLoader.instance.resetSaveData();
    }

    public void DestroySaveSystem()
    {
        instance = null;
        Destroy(gameObject);
    }
}
