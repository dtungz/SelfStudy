using UnityEngine;

public class PlayerPrefSaveService : ISaveService
{
    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public T Load<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(json);
    }

    public bool HasKey(string key) => PlayerPrefs.HasKey(key);

    public void Delete(string key) => PlayerPrefs.DeleteKey(key);
}
