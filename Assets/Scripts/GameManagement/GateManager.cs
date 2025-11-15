using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Abrir");
        animator.SetBool("Abrir", true);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Fechar");
        animator.SetBool("Abrir", false);
    }
}
