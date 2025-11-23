using UnityEngine;

public class PipeItem : MonoBehaviour
{
    private float speed = 3.0f;
    private float rotationSpeed = 100f;
    private float maxHeight = 70f;

    private void Update() {
        transform.position += Vector3.up * speed * Time.deltaTime;

        transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime);

        if(transform.position.y > maxHeight)
            Destroy(gameObject);
    }
}
