using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MenuBase, ISaveable
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider ambientVolumeSlider;
    [SerializeField] Slider sFXVolumeSlider;

    private void Awake() {
    }

    public void SaveConfig() {
        SaveManager.instance.SaveConfig();
    }

    public object GetData() {
        return new VolumesData(musicVolumeSlider.value, ambientVolumeSlider.value, sFXVolumeSlider.value);
    }

    public void SetData(object data) {
        VolumesData volumesData = (VolumesData)data;

        musicVolumeSlider.value = volumesData.musicVolume;
        ambientVolumeSlider.value = volumesData.ambientVolume;
        sFXVolumeSlider.value = volumesData.sfxVolume;
        UpdateSoundManager();
    }

    public void UpdateSoundManager() {
        SoundManager.instance.SetConfig(GetData() as VolumesData);
    }
}
