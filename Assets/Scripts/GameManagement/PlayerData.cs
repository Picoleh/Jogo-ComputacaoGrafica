using UnityEngine;

[System.Serializable]
public class PlayerData{
    public float[] position;
    public float rotationY;

    public PlayerData(float[] position, float rotationY) {
        this.position = position;
        this.rotationY = rotationY;
    }
}
