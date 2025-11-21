using System;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MenuBase, ISaveable
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider ambientVolumeSlider;
    [SerializeField] Slider sFXVolumeSlider;
    [SerializeField] TMP_Dropdown dropdown;

    public void SaveConfig() {
        SaveManager.instance.SaveConfig();
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }

    public object GetData() {
        QualityData quality = new QualityData(dropdown.value);
        VolumesData volumes = new VolumesData(musicVolumeSlider.value, ambientVolumeSlider.value, sFXVolumeSlider.value);
        return new ConfigData(volumes, quality);
    }

    public void SetData(object data) {
        ConfigData config = (ConfigData)data;

        musicVolumeSlider.value = config.volumesData.musicVolume;
        ambientVolumeSlider.value = config.volumesData.ambientVolume;
        sFXVolumeSlider.value = config.volumesData.sfxVolume;

        dropdown.value = config.qualityData.qualityIndex;
        UpdateSoundManager();
    }

    public void UpdateSoundManager() {
        VolumesData volumes = new VolumesData(musicVolumeSlider.value, ambientVolumeSlider.value, sFXVolumeSlider.value);
        SoundManager.instance.SetConfig(volumes);
    }
}
