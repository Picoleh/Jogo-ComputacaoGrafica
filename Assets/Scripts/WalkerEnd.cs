using UnityEngine;

public class WalkerEnd : Walker {
    [SerializeField] private float rotationCorrection = 0f;
    protected override int GetNextIndex() {
        if(++currentIndex >= waypoints.Length)
            return -1;
        return currentIndex;
    }

    protected override void Arrived() {
        base.Arrived();
        transform.Rotate(0, rotationCorrection, 0);
    }
}
