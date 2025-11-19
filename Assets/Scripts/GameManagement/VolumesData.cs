using UnityEngine;

[System.Serializable]
public class VolumesData{
    public float musicVolume;
    public float ambientVolume;
    public float sfxVolume;

    public VolumesData(float musicVolume, float ambientVolume, float sfxVolume) { 
        this.musicVolume = musicVolume;
        this.ambientVolume = ambientVolume;
        this.sfxVolume = sfxVolume;
    }
}
