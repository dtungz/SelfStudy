using System.Collections.Generic;
using UnityEngine;

public class MockSaveService : ISaveService
{
    private Dictionary<string, object> data = new Dictionary<string, object>();
    
    public void Save(string key, object data) => this.data[key] = data;
    public T Load<T>(string key) => (T)data[key];
    public bool HasKey(string key)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(string key)
    {
        throw new System.NotImplementedException();
    }
}


