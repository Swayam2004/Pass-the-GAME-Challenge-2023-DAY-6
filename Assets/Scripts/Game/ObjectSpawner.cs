using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    /* Day3 developer here:
    * This script was written by the previous developers, you should really use my new version
    * "DangersSpawner" as it handles scriptable objects and warning signs at the bottom of the screen
    */
    public GameObject[] objects;
    public float spawnTime;

    private void Start() {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            InstantiateObjects(Random.Range(0, objects.Length));
        }
    }

    private void InstantiateObjects(int index)
    {
        Instantiate(objects[index], new Vector2(Random.Range(-10.0f, 7.0f), transform.position.y), Quaternion.identity);
    }
}
