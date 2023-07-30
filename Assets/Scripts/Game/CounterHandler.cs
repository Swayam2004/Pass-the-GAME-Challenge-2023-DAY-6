using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    public TMP_Text highscoreText;
    // value changed by the game manager, it stands for the falling speed of the player
    [SerializeField] public float metersEachFrame = 0.1f;
    [SerializeField] BGScroll[] bgs;
    public float distance;
    public bool canCount;
    public GameObject information;
    public TMP_Text hs;
    public static float lastDistance = 0;

    void Start()
    {
        highscoreText.text = DistString(PlayerPrefs.GetFloat("Highscore", 0));
        lastScoreText.text = DistString(lastDistance);
    }
    string DistString(float highscore) {
        if(highscore < 999) return highscore.ToString("0") + "m";
        else if(highscore < 99999) return (highscore / 1000).ToString("0.0") + "km";
        else return ((int) (highscore / 1000)).ToString() + "km";
    }
    private void FixedUpdate()
    {
        StartGame start = GameObject.Find("Player").GetComponent<StartGame>();
        if (start.isGameStarted || Input.GetKeyDown("r") && start.canStartGame)
        {
            canCount = true;
        }
           UpdateDistanceText();
            // set the speed for every background that scrolls
            foreach (BGScroll bg in bgs)
        {
            bg.scrollSpeed = metersEachFrame;
        }
    }
    // updates the diamond text
    public void UpdateDiamondText(int amount)
    {   
        diamondText.text = amount.ToString();
    }
    // Update the text and calculates the distance value
    private void UpdateDistanceText()
    {
        if (canCount)
        {
            distance += metersEachFrame * Time.fixedDeltaTime;
            distanceText.text = DistString(distance);
        }
        float high = PlayerPrefs.GetFloat("Highscore", 0);
        if (distance > high)
        {
            PlayerPrefs.SetFloat("Highscore", distance);
            if(high >= 1f) {
                highscoreText.text = Mathf.Round(distance).ToString("0") + "m";
                StartCoroutine(newHighscore(information));
            }
            else hs.text = "";
        }
    }
    IEnumerator newHighscore(GameObject info)
    {
        info.SetActive(true);
        yield return new WaitForSeconds(4);
        hs.text = "";
    }
}
