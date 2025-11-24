using System.Linq;
using UnityEngine;

public class RotaVisualizer : MonoBehaviour
{
    private void OnDrawGizmos() {
        Transform[] waypoints = transform.GetComponentsInChildren<Transform>();
        waypoints = waypoints.Where(t => t != transform).ToArray();

        Gizmos.color = Color.yellow;

        Gizmos.color = Color.cyan;
        foreach (var wp in waypoints) {
            if (wp != null)
                Gizmos.DrawWireSphere(wp.position, 0.3f);
        }
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++) {
            if (waypoints[i] != null && waypoints[i + 1] != null)
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
