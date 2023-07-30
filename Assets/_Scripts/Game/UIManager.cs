using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    /* Day3 developer here:
    * This script was written by the previous developers and it handles the display of hearts.
    * You shouldn't really touch this, at least try to not break it as it is super important for the rest 
    * of the project structure :D
    */

    public GameObject heart;
    [HideInInspector]
    public List<Image> hearts;

    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;

        playerHealth.DamageTaken += UpdateHearts;
        playerHealth.HealthUpgraded += AddHearts;

        for(int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject h = Instantiate(heart, this.transform);
            hearts.Add(h.GetComponent<SingleHeartUI>().GetHeartImage());
        }
        
    }

    private void UpdateHearts()
    {
        int heartFill = playerHealth.Health;

        foreach(Image i in hearts)
        {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }

    }

    private void AddHearts()
    {
        if(hearts.Count <= 14)
        {
            foreach (Image i in hearts)
            {
                Destroy(i.transform.parent.gameObject);
            }

            hearts.Clear();

            for (int i = 0; i < playerHealth.maxHealth; i++)
            {
                GameObject h = Instantiate(heart, this.transform);
                hearts.Add(h.GetComponent<SingleHeartUI>().GetHeartImage());
            }
        }

        UpdateHearts();
    }
}
