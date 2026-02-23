using System.IO;
using UnityEngine;

public class JsonFileSaveService : ISaveService
{
    private string _savePath;

    public JsonFileSaveService(string path)
    {
        _savePath = path;
    }

    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        string filepath = Path.Combine(_savePath, key + ".json");
        File.WriteAllText(filepath, json);
    }

    public T Load<T>(string key)
    {
        string filePath = Path.Combine(_savePath, key + ".json");
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(json);
    }

    public bool HasKey(string key)
    {
        string filePath = Path.Combine(_savePath, key + ".json");
        return File.Exists(filePath);
    }

    public void Delete(string key)
    {
        string filePath = Path.Combine(_savePath, key + ".json");
        File.Delete(filePath);  
    }
}
