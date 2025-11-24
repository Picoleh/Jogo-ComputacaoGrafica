using UnityEngine;

[System.Serializable]
public class MenuData
{
    public float batteryLevel;

    public MenuData(float batteryLevel) {
        this.batteryLevel = batteryLevel;
    }
}
