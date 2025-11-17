using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionUIPrompt _interactionUIPrompt;
    [SerializeField] private Animator animator;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, colliders, _interactableMask);

        if(_numFound > 0) {
            var interactable = colliders[0].GetComponent<IInteractable>();
            if (interactable != null) {
                if (!_interactionUIPrompt.isActive)
                    _interactionUIPrompt.SetUp(interactable.interactionPrompt);
            }
        }
        else {
            if (_interactionUIPrompt.isActive)
                _interactionUIPrompt.Close();
        }

    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    //}

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) {
            if (_numFound > 0) {
                SoundManager.instance.PlaySFX("SomRobo4");
                var interactable = colliders[0].GetComponent<IInteractable>();
                if (interactable != null) {
                    if (interactable is NPC) {
                    }
                    else if(interactable is Item) {
                        animator.SetTrigger("Interact");
                    }
                        interactable.Interact(this);
                }
            }
        }
    }
}
