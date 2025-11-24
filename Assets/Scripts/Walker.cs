using UnityEngine;

public abstract class Walker : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float arriveTreshhold;
    private bool arrived = false;

    protected int currentIndex = 0;

    protected virtual void Update() {
        if(!arrived)
            Move();
    }

    protected void Move() {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) <= arriveTreshhold) {
            currentIndex = GetNextIndex();
        }
    }

    protected abstract int GetNextIndex();
}
