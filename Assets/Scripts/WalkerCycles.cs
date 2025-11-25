using UnityEngine;

public class WalkerCycles : Walker {
    protected override int GetNextIndex() {
        return (currentIndex + 1) % waypoints.Length;
    }

    protected override void OnCheckpoint() {
    }
}
