using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject Manager;
    public GameObject Spawners;
    public GameObject CounterManager;
    public GameObject menu;
    public bool canStartGame = true;
    public bool isGameStarted = false;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r") && canStartGame)
        {
            Upgrades.Instance.source.PlayOneShot(Upgrades.Instance.select);

            Manager.SetActive(true);
            Spawners.SetActive(true);
            CounterManager.SetActive(true);
            menu.SetActive(false);

            GetComponent<Player>().enabled = true;
            GetComponent<PlayerCollision>().enabled = true;

            canStartGame = false;
            isGameStarted = true;
        }
    }
}
