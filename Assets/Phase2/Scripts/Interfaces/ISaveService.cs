using UnityEngine;

public interface ISaveService
{
    public void Save(string key, object data);
    public T Load<T>(string key);
    public bool HasKey(string key);
    public void Delete(string key);
}
