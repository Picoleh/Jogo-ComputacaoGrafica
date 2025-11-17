using UnityEngine;
using System.Collections;

public class AnimalBehaviorCollider : MonoBehaviour {
    [Header("Movimento")]
    public float moveSpeed = 2f;
    public float moveTimeMin = 3f;
    public float moveTimeMax = 6f;
    public float obstacleDetectDistance = 5f; // distância pra detectar a cerca

    [Header("Comer")]
    public float eatDuration = 3f;
    public float eatChance = 0.3f;

    private Vector3 moveDirection;
    private bool isEating = false;

    void Start() {
        PickNewDirection();
        StartCoroutine(BehaviorLoop());
    }

    IEnumerator BehaviorLoop() {
        while (true) {
            if (!isEating) {
                if (Random.value < eatChance) {
                    yield return StartCoroutine(Eat());
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

        while (elapsed < moveTime) {
            // Raycast para frente para detectar a cerca
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, moveDirection, obstacleDetectDistance)) {
                PickNewDirection();
            }

            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 5f * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        PickNewDirection();
    }

    IEnumerator Eat() {
        isEating = true;
        yield return new WaitForSeconds(eatDuration);
        isEating = false;
        PickNewDirection();
    }

    void PickNewDirection() {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    //private void OnDrawGizmosSelected() {
    //    // Desenha o raycast para debug
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + moveDirection * obstacleDetectDistance);
    //}
}