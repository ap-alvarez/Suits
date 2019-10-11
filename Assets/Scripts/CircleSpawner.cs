using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : EnemySpawner
{
    public float radius;
    public float angle;
    public float spawnRate;

    private float spawnWait = 0;
    // Update is called once per frame
    void Update()
    {
        spawnWait += Time.deltaTime;

        if (spawnWait >= 1f / spawnRate)
        {
            spawnWait = 0;

            Spawn(GetSpawnLocation(Random.Range(0f, angle * Mathf.Deg2Rad)), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnLocation(float radAngle)
    {
        Vector3 localPos = new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle));
        localPos *= radius;

        Vector3 worldPos = transform.TransformPoint(localPos);

        return worldPos;
    }
}
