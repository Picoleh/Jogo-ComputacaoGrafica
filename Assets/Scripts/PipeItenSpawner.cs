using System.Collections.Generic;
using UnityEngine;

public class PipeItenSpawner : MonoBehaviour
{
    [SerializeField] private List<PipeItem> objectsToSpawn;
    [SerializeField] private float spawnDelay = 1f;
    private float timer = 0f;

    private void Update() {
        if (timer < spawnDelay) {
            timer += Time.deltaTime;
        }
        else {
            Instantiate(objectsToSpawn[Random.Range(0,objectsToSpawn.Count)], new Vector3(64f, 17f, -103.7f), Quaternion.identity);
            timer = 0f;
        }
    }

}
