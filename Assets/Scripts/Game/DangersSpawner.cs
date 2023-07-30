using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangersSpawner : MonoBehaviour
{
    /* Day3 developer here:
     * This is the script that is used to spawn every object that spawns every x seconds
     * It works by using my custom data type ( a scriptable object ) with holds all the info
     * you need to spawn an object.
     */
    [Header("Settings")]
    [SerializeField] public float spawntime;
    [SerializeField] float yWarningSpawnPosition;
    [SerializeField] bool useaudio;
    [SerializeField] float timesfxAfterSpawn;

    [Header("References")]
    [SerializeField] WarningObject[] dangers;
    [SerializeField] GameObject warningPrefab;

    [SerializeField] AudioSource source;
    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawntime);
            SpawnDanger(dangers[Random.Range(0,dangers.Length)]);
        }
    }

    private void SpawnDanger(WarningObject warningObject)
    {
        float randomX = Random.Range(-10.0f, 7.0f);
        if (warningObject.useWarning)
        {
            // Spawn a warning
            GameObject warning = Instantiate(warningPrefab, new Vector2(randomX, yWarningSpawnPosition), Quaternion.identity);
            Animator anim = warning.GetComponent<Animator>();
            // Handle warning animations
            anim.SetBool("Incoming", false);
            StartCoroutine(SetWaringToIncoming(anim, warningObject,warning));
            // Spawn the object and destroy the warning
            StartCoroutine(SpawnObject(warningObject, new Vector2(randomX, transform.position.y - 1)));
        }
        else
        {
            // spawn the object
            StartCoroutine(SpawnObject(warningObject, new Vector2(randomX, transform.position.y - 1)));
        }
    }

    IEnumerator SetWaringToIncoming(Animator anim, WarningObject warningObject, GameObject warning)
    {
        yield return new WaitForSeconds(warningObject.spawnTime);
        anim.SetBool("Incoming", true);
        yield return new WaitForSeconds(1.3f);
        Destroy(warning);
    }

    IEnumerator SpawnObject(WarningObject warningObject,Vector2 pos)
    {
        yield return new WaitForSeconds(warningObject.spawnTime);
        Instantiate(warningObject.graphics,pos, Quaternion.identity);
        yield return new WaitForSeconds(timesfxAfterSpawn);
        if(warningObject.spawnsfx != null)
        {
            source.PlayOneShot(warningObject.spawnsfx);
        }
    }
}
