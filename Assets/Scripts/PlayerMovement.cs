using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private GameObject inventoryUI;
    private Vector3 velocity;
    private float rotateInput;

    [SerializeField] private InputActionReference move;
    [SerializeField] private bool isSprinting;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        velocity = Vector3.zero;
        isSprinting = false;
    }

    // Update is called once per frame
    void Update(){

        if (Mathf.Abs(rotateInput) > 0.01f) {
            float rotationPenalty = isSprinting ? 0.4f : 1.0f;
            transform.Rotate(Vector3.up, rotateInput * (rotationSpeed * rotationPenalty) * Time.deltaTime);
        }

        if (move == null || move.action == null) {
            Debug.LogWarning("Move InputActionReference is null!");
            return;
        }
        float speed = isSprinting ? _sprintSpeed : moveSpeed;
        Vector2 _moveDirInput = move.action.ReadValue<Vector2>();
        var horizontalMove = new Vector3(_moveDirInput.x, 0, _moveDirInput.y);
        horizontalMove = transform.TransformDirection(horizontalMove);
        horizontalMove = horizontalMove * speed;
        animator.SetFloat("Speed", horizontalMove.magnitude);


        velocity = new Vector3(horizontalMove.x, velocity.y, horizontalMove.z);

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed && Mathf.Abs(velocity.x) > 0) {
            isSprinting = true;
        }
        else if (context.canceled) {
            isSprinting = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context) {
            rotateInput = context.ReadValue<float>();
    }

    public void OnOpenInventory(InputAction.CallbackContext context) {
        if (context.performed){
            InventoryManager.instance.OpenInventory();
        }
    }

    public void OnNextSentence(InputAction.CallbackContext context) {
        if (context.performed) {
            DialogueSystem.instance.DisplayNextSentence();
        }
    }


}
