using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapManager : MonoBehaviour
{
    public static InputMapManager instance { get; private set; }

    [SerializeField] private InputActionAsset inputActions;

    private string _currentActiveMap = null;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        EnableMap("Gameplay");
    }

    public void EnableMap(string mapName) {
        foreach (var map in inputActions.actionMaps) { 
            map.Disable();
        }

        inputActions.FindActionMap(mapName).Enable();
        _currentActiveMap=mapName;
    }

    public void DisableControls() {
        inputActions.FindActionMap(_currentActiveMap).Disable();
    }

    public void EnableControls() {
        inputActions.FindAction(_currentActiveMap).Enable();
    }
}
