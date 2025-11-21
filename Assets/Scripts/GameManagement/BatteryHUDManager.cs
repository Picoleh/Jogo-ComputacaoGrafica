using UnityEngine;
using UnityEngine.UI;

public class BatteryHUDManager : MonoBehaviour
{
    [SerializeField] private BatteryManager battery;
    [SerializeField] private Image fillBar;

    [Header("Cores")]
    public Color highColor = new Color(0f, 0.90f, 0.45f);     // verde neon
    public Color midColor = new Color(1f, 0.93f, 0.23f);      // amarelo
    public Color lowColor = new Color(1f, 0.09f, 0.27f);      // vermelho

    private float smoothValue = 1f;

    void Update() {
        float target = battery.currentBattery / battery.maxBattery;
        smoothValue = Mathf.Lerp(smoothValue, target, Time.deltaTime * 5f);

        fillBar.fillAmount = smoothValue;
        fillBar.color = GetColorForPercent(target);
    }

    private Color GetColorForPercent(float p) {
        if (p > 0.6f)
            return highColor;
        if (p > 0.3f)
            return midColor;
        else
            return lowColor;
    }
}
