using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shop : MonoBehaviour
{
    public GameObject shop;
    public GameObject menu;
    public TMP_Text diamondsText;
    // Menu animation :D
    public Animator menuAnim;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        // Diamonds text in shop will change only if you die. So if you change it in inspector while in play mode, diamonds value will change but text no, so don't be scare by this :D. 
        // Everything works fine!

        GetComponent<Player>().diamondsCollected = PlayerPrefs.GetInt("Diamonds");
        diamondsText.text = PlayerPrefs.GetInt("Diamonds").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        diamondsText.text = PlayerPrefs.GetInt("Diamonds").ToString();
        if (Input.GetKeyDown("c") && !isOpen && GetComponent<StartGame>().canStartGame == true && GetComponent<StartGame>().isGameStarted == false)
        {
            Upgrades.instance.source.PlayOneShot(Upgrades.instance.select);
            shop.SetActive(true);
            menu.SetActive(false);
            menuAnim.Play("Base Layer.Menu", 0);
            isOpen = true;
            GetComponent<StartGame>().canStartGame = false;
        }
        else if (Input.GetKeyDown("c") && isOpen && GetComponent<StartGame>().canStartGame == false && GetComponent<StartGame>().isGameStarted == false)
        {
            Upgrades.instance.source.PlayOneShot(Upgrades.instance.select);
            shop.SetActive(false);
            menu.SetActive(true);
            menuAnim.Play("Base Layer.Menu", 0);
            isOpen = false;
            GetComponent<StartGame>().canStartGame = true;
        }
    }
    public void MakeAPurchase(int amount)
    {
        GetComponent<Player>().diamondsCollected -= amount;
        PlayerPrefs.SetInt("Diamonds", GetComponent<Player>().diamondsCollected);
        diamondsText.text = GetComponent<Player>().diamondsCollected.ToString();
    }
}
