using UnityEngine;

[System.Serializable]
public class ConfigData{
    public VolumesData volumesData;
    public QualityData qualityData;
    
    public ConfigData(VolumesData volumes, QualityData qualityData) {
        volumesData = volumes;
        this.qualityData = qualityData;
    }
}
