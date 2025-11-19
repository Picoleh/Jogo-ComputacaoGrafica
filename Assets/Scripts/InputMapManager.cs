using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapManager : MonoBehaviour
{
    public static InputMapManager instance { get; private set; }

    [SerializeField] private InputActionAsset inputActions;
    private PlayerMovement player;
    private string _currentActiveMap = null;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void GetInputReferences() {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    public void OnRotate(InputAction.CallbackContext context) {
        player.Rotate(context.ReadValue<float>());
    }

    public void OnOpenInventory(InputAction.CallbackContext context) {
        if (context.performed)
            InventoryManager.instance.OpenInventory();
    }

    public void OnCloseInventory(InputAction.CallbackContext context) {
        if (context.performed)
            InventoryManager.instance.OnCloseInventory();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed)
            player.Interact();
    }

    public void OnSprint(InputAction.CallbackContext context) {
        player.Sprint(context);
    }

    public void OnNextSentence(InputAction.CallbackContext context) {
        if(context.performed)
            DialogueSystem.instance.DisplayNextSentence();
    }

    public void OnOpenPauseMenu(InputAction.CallbackContext context) {
        if(context.performed)
            MenuManager.instance.OpenMenu(MenuType.Pause);
    }
}
