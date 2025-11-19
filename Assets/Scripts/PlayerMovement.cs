using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour, ISaveable{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private float acceleration = 12.0f;
    [SerializeField] private float maxMoveSpeed = 6.0f;
    [SerializeField] private float maxSprintSpeed = 12.0f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private GameObject inventoryUI;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;
    private float rotateInput;

    [SerializeField] private InputActionReference move;
    [SerializeField] private bool isSprinting;


    private void Awake() {
        SaveManager.instance.RegisterPlayer(this);
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        horizontalVelocity = Vector3.zero;
        verticalVelocity = 0;
        isSprinting = false;
    }

    void Update(){

        if (Mathf.Abs(rotateInput) > 0.01f) { // Testa se há input de rotação
            // Aplica penalização se estiver correndo
            float rotationPenalty = isSprinting ? 0.4f : 1.0f; 
            // Calcula quantidade
            float rotationAmount = rotateInput * rotationSpeed * rotationPenalty * Time.deltaTime;
            // Altera a velocidade com base na rotação
            horizontalVelocity = Quaternion.Euler(0, rotationAmount, 0) * horizontalVelocity;
            // Rotaciona o modelo
            transform.Rotate(0, rotationAmount, 0);
        }

        // Lê o input de movimento
        Vector2 _moveDirInput = move.action.ReadValue<Vector2>();
        var horizontalMove = new Vector3(_moveDirInput.x, 0, _moveDirInput.y);

        // Transforma velocidade global para local
        Vector3 localVelocity = transform.InverseTransformDirection(horizontalVelocity);

        float maxSpeed = isSprinting ? maxSprintSpeed : maxMoveSpeed;
        float currentSpeed = horizontalVelocity.magnitude;

        if (horizontalMove.magnitude > 0.1f) { // Se houve input

            Vector3 localForward = transform.forward * _moveDirInput.y + transform.right * _moveDirInput.x;
            float customAcceleration = acceleration;

            localForward.Normalize();

            if(currentSpeed < maxSpeed) {
                customAcceleration *= Mathf.Lerp(6f, 1f, currentSpeed / maxSpeed);
            }

            // Produto vetorial para ver se nova direção é contraria a atual
            float dot = Vector3.Dot(horizontalVelocity.normalized, localForward);

            if (dot < -0.1f) // Se dot < 0 --> direção contraria
                customAcceleration *= 6;

            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, localForward * maxSpeed, customAcceleration * Time.deltaTime);
        }
        else { // Sem inputs
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, acceleration * 6 * Time.deltaTime);
        }

        animator.SetFloat("Speed", currentSpeed);
        if(currentSpeed > maxMoveSpeed) {
            SoundManager.instance.PlayLoop("RoboCorrendo");
        }
        else if(currentSpeed > 1.0f) {
            SoundManager.instance.PlayLoop("RoboAndando");
        }
        else {
            SoundManager.instance.StopLoop();
        }

        Vector3 finalVelocity = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z);

        if (controller.isGrounded && verticalVelocity < 0) {
            verticalVelocity = -2f;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        controller.Move(finalVelocity * Time.deltaTime);
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed && horizontalVelocity.magnitude > 0) {
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

    public void OnOpenPauseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            PauseMenuManager.instance.OpenMenu();
        }
    }

    public object GetData() {
        return new PlayerData(
            new float[] {
                transform.position.x,
                transform.position.y,
                transform.position.z
            },
            transform.eulerAngles.y
        );
    }

    public void SetData(object data) {
        controller.enabled = false;
        PlayerData playerData = (PlayerData)data;
        
        transform.SetPositionAndRotation(new Vector3(
                playerData.position[0],
                playerData.position[1],
                playerData.position[2]
            ), Quaternion.Euler(0, playerData.rotationY, 0));

        controller.enabled = true;
    }
}
