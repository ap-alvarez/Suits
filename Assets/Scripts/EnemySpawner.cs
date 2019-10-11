using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Terrain terrain;

    /// <summary>
    /// Spawns enemy at world position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="rotation"></param>
    protected void Spawn(Vector3 worldPosition, Quaternion rotation)
    {
        EnemyBehaviour newEnemy = (Instantiate(enemy, new Vector3(worldPosition.x, GetSpawnHeight(worldPosition), worldPosition.z), rotation) as GameObject).GetComponent<EnemyBehaviour>();
        newEnemy.SetPlayer(this.player);
    }

    protected float GetSpawnHeight(Vector3 worldPos)
    {
        Vector3 terrainPos = terrain.transform.InverseTransformPoint(worldPos);
        float terrainHeight = terrain.terrainData.GetInterpolatedHeight(terrainPos.x / 1000, terrainPos.z / 1000);

        return terrainHeight + (enemy.GetComponent<CharacterController>().height * enemy.transform.localScale.y);
    }
}
