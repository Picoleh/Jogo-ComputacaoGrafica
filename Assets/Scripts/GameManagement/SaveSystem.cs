using System.IO;
using UnityEngine;

public class SaveSystem{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(GameData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Game saved to " + path);
    }

    public static GameData Load() {
        if (!File.Exists(path)) {
            Debug.LogWarning("Save file not found at " + path);
            return default;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameData>(json);
    }
}
