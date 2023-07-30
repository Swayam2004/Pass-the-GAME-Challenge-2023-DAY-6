using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public GameObject fasterButton, diamondButton, fallButton, healthButton;
    public GameObject information;
    public static bool fasterPlayer = false;
    public static bool moreDiamonds = false;
    public static bool fasterFalling = false;
    public static bool health = false;
    public AudioClip select;
    public AudioSource source;
    public static Upgrades instance;
    public DangersSpawner diamondsSpawner;

    // Sorry, I don't know other way to save purchased things : (((. Maybe you know :D
    void Start()
    {
        instance = this;
        fasterPlayer = PlayerPrefs.GetInt("Faster", 0) == 1;
        moreDiamonds = PlayerPrefs.GetInt("MoreDiamonds", 0) == 1;
        fasterFalling = PlayerPrefs.GetInt("Fall", 0) == 1;
        health = PlayerPrefs.GetInt("Health", 0) == 1;
        if(fasterPlayer) {
            GetComponent<Player>().moveSpeed = 15;
            fasterButton.SetActive(false);
        }
        if(moreDiamonds)
            diamondButton.SetActive(false);
        
        if(fasterFalling)
            fallButton.SetActive(false);
        if(health) {
            GetComponent<PlayerHealth>().maxHealth = 5;
            GetComponent<PlayerHealth>().health = 5;
            healthButton.SetActive(false);
        }
    }
    public void BuyFasterPlayer()
    {
        source.PlayOneShot(select);
        if (GetComponent<Player>().diamondsCollected >= 60)
        {
            GetComponent<Shop>().MakeAPurchase(60);
            GetComponent<Player>().moveSpeed = 15;
            fasterButton.SetActive(false);
            fasterPlayer = true;
            PlayerPrefs.SetInt("Faster", 1);
        }
        else NotEnough();
    }
    // This is from the Day 5 developer. Upgrades should be as simple
    // as a new variable, button, function, price, and feature.
    // Sounds like a lot, but it should take 15 minutes maximum.
    public void BuyMoreDiamonds()
    {
        source.PlayOneShot(select);
        if (GetComponent<Player>().diamondsCollected >= 100)
        {
            GetComponent<Shop>().MakeAPurchase(100);
            diamondButton.SetActive(false);
            moreDiamonds = true;
            PlayerPrefs.SetInt("MoreDiamonds", 1);
        }
        else NotEnough();
    }
    public void FasterFalling()
    {
        source.PlayOneShot(select);
        if (GetComponent<Player>().diamondsCollected >= 90)
        {
            GetComponent<Shop>().MakeAPurchase(90);
            fallButton.SetActive(false);
            fasterFalling = true;
            PlayerPrefs.SetInt("Fall", 1);
        }
        else NotEnough();
    }
    public void BuyHealth()
    {
        source.PlayOneShot(select);
        if (GetComponent<Player>().diamondsCollected >= 160)
        {
            GetComponent<Shop>().MakeAPurchase(160);
            healthButton.SetActive(false);
            health = true;
            GetComponent<PlayerHealth>().maxHealth = 5;
            GetComponent<PlayerHealth>().health = 5;
            PlayerPrefs.SetInt("Health", 1);
        }
        else NotEnough();
    }

    public void NotEnough()
    {
        StartCoroutine(notenough(information));
    }

    IEnumerator notenough(GameObject info)
    {
        info.SetActive(true);
        yield return new WaitForSeconds(3);
        info.SetActive(false);
    }
    public void ResetProgress() {
        source.PlayOneShot(select);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
