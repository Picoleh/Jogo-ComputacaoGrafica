using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WalkerPause : Walker {
    [SerializeField] private float timeStopped = 5f;
    private float currentTimeStopped = 0f;
    private bool isStopped = false;
    protected override int GetNextIndex() {
        return (currentIndex + 1) % waypoints.Length;
    }

    protected override void OnCheckpoint() {
        isStopped = true;
        currentTimeStopped = 0f;
        animator.SetTrigger("Idle");
    }

    protected override void Move() {
        if (isStopped) {
            if (currentTimeStopped < timeStopped) {
                currentTimeStopped += Time.deltaTime;
            }
            else {
                animator.SetTrigger("Walking");
                isStopped = false;
            }
        }
        else {
            base.Move();
        }
    }
}
