using System;
using UnityEngine;

public class WalkerPingPong : Walker {
    [SerializeField] private float timeStopped = 5f;
    [SerializeField] private float rotationOnFinal = 0f;
    private int inc = 1;
    private float currentTimeStopped = 0f;
    private bool isStopped = false;
    protected override int GetNextIndex() {
        if (currentIndex >= waypoints.Length - 1) {
            inc = -1;
            FinalCheckpoint();
        }
        if (currentIndex <= 0) {
            inc = 1;
            FirstCheckpoint();
        }
        currentIndex += inc;
        return currentIndex;
    }

    private void FirstCheckpoint() {
        isStopped = true;
        currentTimeStopped = 0f;
        animator.SetTrigger("First");
    }

    private void FinalCheckpoint() {
        transform.Rotate(0, rotationOnFinal, 0);
        isStopped = true;
        currentTimeStopped = 0f;
        animator.SetTrigger("Final");
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

    protected override void OnCheckpoint() {
    }
}
