using UnityEngine;
using System.IO;

public class DataLoader
{

    private const string FILE_NAME = "Save";

    public static T LoadData<T>()
    {
        return JsonUtility.FromJson<T>(Resources.Load<TextAsset>(FILE_NAME).text);
    }

    public static void SaveData(object obj)
    {
        var path = Path.Combine(Application.dataPath, FILE_NAME);

        File.WriteAllText(path, JsonUtility.ToJson(obj));
    }
}
