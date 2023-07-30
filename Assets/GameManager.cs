using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Curves")]
    [SerializeField] AnimationCurve dangerSpawnTimeCurve;
    [SerializeField] AnimationCurve objectSpawnTimeCurve;
    [SerializeField] AnimationCurve fallVelocityCurve;


    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] DangersSpawner dangersSpawner;
    [SerializeField] DangersSpawner objectSpawner;
    [SerializeField] CounterHandler counterHandler;

    float timer;
    float distance;
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (GameObject.Find("CounterManager").GetComponent<CounterHandler>().canCount == true)
        {
            timer += Time.deltaTime;
            distance = counterHandler.distance;
        }
        if (playerHealth.isdead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        HandleCurves();
    }

    private void HandleCurves()
    {
        dangersSpawner.spawntime = dangerSpawnTimeCurve.Evaluate(distance);
        objectSpawner.spawntime = objectSpawnTimeCurve.Evaluate(distance) * (Upgrades.moreDiamonds ? 0.4f : 1f);
        if (GameObject.Find("CounterManager").GetComponent<CounterHandler>().canCount == true)
        {
        counterHandler.metersEachFrame = fallVelocityCurve.Evaluate(timer) * (Upgrades.fasterFalling ? 1.6f : 1f);
        }
    }
}
