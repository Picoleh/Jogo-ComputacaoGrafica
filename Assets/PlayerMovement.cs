using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed;
    public float jumpHeight;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    private Vector3 velocity;

    public InputActionReference move;
    public InputActionReference jump;

    private Transform mainCameraTransform;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        mainCameraTransform = Camera.main.transform;
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update(){

        

        Vector2 _moveDirInput = move.action.ReadValue<Vector2>();
        var horizontalMove = new Vector3(_moveDirInput.x, 0, _moveDirInput.y);
        horizontalMove = mainCameraTransform.TransformDirection(horizontalMove);

        velocity = new Vector3(horizontalMove.x, velocity.y, horizontalMove.z);

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * moveSpeed * Time.deltaTime);
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

            if (isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }
}
