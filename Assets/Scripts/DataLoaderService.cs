using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine;

public class DataLoaderService
{
    public void DeleteSave(string relativePath)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {
            Debug.Log("something went wrong");
        }
    }

    public bool SaveData<T>(string relativePath, T data)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);

            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception)
        {
            Debug.Log("something went wrong");
            return false;
        }
    }

    public bool SaveDataAsync<T>(string fullPath, T data)
    {
        try
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            using FileStream stream = File.Create(fullPath);

            stream.Close();
            File.WriteAllText(fullPath, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception)
        {
            Debug.Log("something went wrong");
            return false;
        }
    }

    public bool GetDataLoadedAsync<T>(string fullPath, out T data)
    {
        if (!File.Exists(fullPath))
        {
            data = default;
            return false;
        }

        try
        {
            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullPath));

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"failed loading data {ex.Message} {ex.StackTrace}");
            throw ex;
        }
    }

    public bool GetDataLoaded<T>(string relativePath, out T data)
    {
        string path = Application.persistentDataPath + relativePath;

        if (!File.Exists(path))
        {
            data = default;
            return false;
        }

        try
        {
            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"failed loading data {ex.Message} {ex.StackTrace}");
            throw ex;
        }
    }
}
