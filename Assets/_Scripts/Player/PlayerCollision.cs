using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hitsfx;
    [SerializeField] AudioClip collectsfx;
    private Player player;
    private PlayerHealth health;
    private CounterHandler ui;

    public ParticleSystem redHeart;
    public ParticleSystem diamond;
    public ParticleSystem emptyHeart;
    public ParticleSystem rocketCollision;

    public Animator anim;
    private void Start() {
        player = GetComponent<Player>();
        health = GetComponent<PlayerHealth>();
        ui = FindObjectOfType<CounterHandler>();
        ui.UpdateDiamondText(player.diamondsCollected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // this handles every collsion the player has
        // the events called here are also used in the UIManager script
        // add here eventual power ups / abilities

        // take damage if you collide with "danger"
        // take 2 damage if you collide with real danger.

        

        if (other.gameObject.CompareTag("Danger"))
        {
            GameObject currentMissle = other.gameObject;
            health.TakeDamage();
            Destroy(currentMissle);
            source.PlayOneShot(hitsfx);
            Instantiate(rocketCollision, other.gameObject.transform.position, Quaternion.identity);
        }
        if (other.gameObject.CompareTag("REAL. Danger."))
        {
            GameObject currentMissle = other.gameObject;
            health.TakeDamage();
            health.TakeDamage();
            Destroy(currentMissle);
            source.PlayOneShot(hitsfx);
            Instantiate(rocketCollision, other.gameObject.transform.position, Quaternion.identity);
        }

        // Gain a daimond if you collide with one
        if (other.gameObject.CompareTag("Diamond"))
        {
            player.diamondAmount++;
            int amount = player.diamondAmount;
            ui.UpdateDiamondText(player.diamondsCollected + amount);
            anim.Play("Base Layer.Up", 0);
            GameObject currentDiamond = other.gameObject;
            Destroy(currentDiamond);
            source.PlayOneShot(collectsfx);
            // Destroying particles in different script because I couldn't get it work here! (Day 4 dev :D)
            Instantiate(diamond, other.gameObject.transform.position, Quaternion.identity);
        }

        // Gain health if you collide with one hearth
        if (other.gameObject.CompareTag("Hearth"))
        {
            GameObject currentHearth = other.gameObject;
            Destroy(currentHearth);
            health.Heal();
            source.PlayOneShot(collectsfx);
            Instantiate(redHeart, other.gameObject.transform.position, Quaternion.identity);
        }

        // Gain maxhealth if you collide with one hearth+
        if (other.gameObject.CompareTag("Hearth+"))
        {
            GameObject currentHearth = other.gameObject;
            Destroy(currentHearth);
            health.UpgradeHealth();
            source.PlayOneShot(collectsfx);
            Instantiate(emptyHeart, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
