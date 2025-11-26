using System;
using System.Linq;
using UnityEngine;

public abstract class Walker : MonoBehaviour
{
    [SerializeField] protected Transform waypointsRoot;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float arriveTreshhold = 0.3f;
    [SerializeField] private float rotationSpeed = 3.0f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool autoStart;

    protected Transform[] waypoints;
    protected int currentIndex = 0;
    protected bool arrived = false;
    protected bool started = false;

    private void Awake() {
        waypoints = waypointsRoot.GetComponentsInChildren<Transform>();
        waypoints = waypoints.Where(t => t != waypointsRoot).ToArray(); // ignora o próprio root

        if(autoStart)
            startWalker();
    }

    protected virtual void Update() {
        if(!arrived && started)
            Move();
    }

    public void startWalker() {
        animator.SetTrigger("Walking");
        started = true;
    }

    protected virtual void Move() {
        if (waypoints == null || waypoints.Length == 0) 
            return;

        Transform target = waypoints[currentIndex];

        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;

        // 2. Calcula rotação desejada
        Quaternion targetRot = Quaternion.LookRotation(dir);

        // 3. Rotaciona suavemente
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );

        // 4. Move SOMENTE na direção que ele está olhando
        transform.position += transform.forward * speed * Time.deltaTime;


        if (Vector3.Distance(transform.position, target.position) <= arriveTreshhold) {
            OnCheckpoint();
            currentIndex = GetNextIndex();
            if(currentIndex == -1) {
                Arrived();
            }
        }
    }

    //private void OnDrawGizmosSelected() {
    //    if (waypointsRoot == null) return;

    //    foreach (Transform child in waypointsRoot) {
    //        Gizmos.DrawWireSphere(child.position, arriveTreshhold);
    //    }
    //}

    protected abstract int GetNextIndex();
    protected virtual void Arrived() {
        arrived = true;
        animator.SetTrigger("Idle");
    }

    protected abstract void OnCheckpoint();
}
