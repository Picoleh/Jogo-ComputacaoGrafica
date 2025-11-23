using UnityEngine;
using System.Collections;

public class AnimalBehaviorCollider : MonoBehaviour {
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveTimeMin = 3f;
    [SerializeField] private float moveTimeMax = 6f;
    [SerializeField] private float obstacleDetectDistance = 5f; // distância pra detectar a cerca
    [SerializeField] private float eatDuration = 6f;
    [SerializeField] private float idleDuration = 3f;

    private Vector3 moveDirection;
    private bool isEating = false;
    private bool isIdleing = false;

    void Start() {
        PickNewDirection();
        StartCoroutine(BehaviorLoop());
    }

    IEnumerator BehaviorLoop() {
        while (true) {
            if (!isEating && !isIdleing) {
                float r = Random.value;
                if (r < 0.3f) {
                    yield return StartCoroutine(Eat());
                }
                else if(r >= 0.3f && r < 0.6f){
                    yield return StartCoroutine(Idle());
                }
                else {
                    yield return StartCoroutine(Walk());
                }
            }
            yield return null;
        }
    }

    IEnumerator Walk() {
        float moveTime = Random.Range(moveTimeMin, moveTimeMax);
        float elapsed = 0f;

        animator.SetTrigger("Walking");
        while (elapsed < moveTime) {
            // Raycast para frente para detectar a cerca
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, moveDirection, obstacleDetectDistance)) {
                PickNewDirection();
            }

            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 2f * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        PickNewDirection();
    }

    IEnumerator Eat() {
        animator.SetTrigger("Eating");
        isEating = true;
        yield return new WaitForSeconds(eatDuration);
        isEating = false;
        PickNewDirection();
    }

    IEnumerator Idle() {
        animator.SetTrigger("Idle");
        isIdleing = true;
        yield return new WaitForSeconds(idleDuration);
        isIdleing = false;
        PickNewDirection();
    }

    void PickNewDirection() {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnDrawGizmosSelected() {
        // Desenha o raycast para debug
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + moveDirection * obstacleDetectDistance);
    }
}