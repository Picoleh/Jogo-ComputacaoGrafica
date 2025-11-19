using UnityEngine;

[System.Serializable]
public class ConfigData{
    public VolumesData volumesData;
    
    public ConfigData(VolumesData volumes) {
        volumesData = volumes;
    }
}
