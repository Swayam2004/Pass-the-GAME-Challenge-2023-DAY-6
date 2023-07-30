using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemSpawner : MonoBehaviour
{

    /* Day3 developer here:
    * This is the script that is used to spawn every object that spawns with a certain probability overy x seconds
    * I use it to spawn health, you should use this if you want to spawn power ups
    * It works by using my custom data type ( a scriptable object ) with holds all the info
    * you need to spawn an object.
    */
    [Header("Settings")]
    // the time between each try to spawn
    [SerializeField] public float tryToSpawnTime;
    // the percentage of success
    [Range(0f,100f)]
    [SerializeField] public float probability;


    [Header("References")]
    [SerializeField] WarningObject item;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(tryToSpawnTime);
            if (Random.Range(0f, 100f) < probability)
            {
                SpawnDanger(item);
            }
        }
    }

    private void SpawnDanger(WarningObject warningObject)
    {
        float randomX = Random.Range(-10.0f, 7.0f);
        StartCoroutine(SpawnObject(warningObject, new Vector2(randomX, transform.position.y - 1)));
    }

    IEnumerator SpawnObject(WarningObject warningObject, Vector2 pos)
    {
        yield return new WaitForSeconds(warningObject.spawnTime);
        Instantiate(warningObject.graphics, pos, Quaternion.identity);
    }
}
