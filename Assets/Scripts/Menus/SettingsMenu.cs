using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MenuBase, ISaveable
{
    [SerializeField] Slider musicVolumeSlider;

    private void Awake() {
    }

    public void SaveConfig() {
        SaveManager.instance.SaveConfig();
    }

    public object GetData() {
        return new VolumesData(musicVolumeSlider.value);
    }

    public void SetData(object data) {
        VolumesData volumesData = (VolumesData)data;

        musicVolumeSlider.value = volumesData.musicVolume;
        UpdateSoundManager();
    }

    public void UpdateSoundManager() {
        SoundManager.instance.SetConfig(GetData() as VolumesData);
    }
}
