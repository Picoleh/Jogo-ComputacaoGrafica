using System;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField] public float maxBattery = 100f;
    [SerializeField] private float drainRate = 1f;
    public float currentBattery;
    [SerializeField] private bool isUsingBattery;

    public Action<float> OnBatteryChanged;

    void Start() {
        Reset();
    }

    void Update() {
        if (!isUsingBattery || currentBattery <= 0)
            return;

        currentBattery -= drainRate * Time.deltaTime;

        if (currentBattery <= 0) {
            BatteryZero();
        }

        OnBatteryChanged?.Invoke(currentBattery);
    }

    private void BatteryZero() {
        Debug.Log("Bateria zerada!");
        MenuManager.instance.OpenMenu(MenuType.GameOver, GameOverType.LostByBattery);
    }

    public void Recharge(float amount) {
        currentBattery += amount;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        OnBatteryChanged?.Invoke(currentBattery);
    }

    public void SetUsing(bool value) {
        isUsingBattery = value;
    }

    public void Reset() {
        currentBattery = maxBattery;
    }
}
