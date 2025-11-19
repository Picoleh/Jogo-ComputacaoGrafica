using System.IO;
using UnityEngine;

public class SaveSystem{
    private static string gameSavePath = Application.persistentDataPath + "/save.json";
    private static string configSavePath = Application.persistentDataPath + "/config.json";

    public static void Save(GameData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(gameSavePath, json);
        Debug.Log("Game saved to " + gameSavePath);
    }

    public static void Save(ConfigData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(configSavePath, json);
        Debug.Log("Config saved to " + configSavePath);
    }

    public static GameData LoadGameSave() {
        if (!File.Exists(gameSavePath)) {
            Debug.LogWarning("Game Save file not found at " + gameSavePath);
            return default;
        }

        string json = File.ReadAllText(gameSavePath);
        return JsonUtility.FromJson<GameData>(json);
    }

    public static ConfigData LoadConfigSave() {
        if (!File.Exists(configSavePath)) {
            Debug.LogWarning("Config Save file not found at " + configSavePath);
            return default;
        }

        string json = File.ReadAllText(configSavePath);
        return JsonUtility.FromJson<ConfigData>(json);
    }
}
