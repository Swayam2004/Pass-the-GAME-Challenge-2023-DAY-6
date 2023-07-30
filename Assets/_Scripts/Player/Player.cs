using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float lerpValue = 0.2f;
    private Rigidbody2D rb;

    public float tiltAngle = 15f;
    public float tiltSpeed = 0.2f;
    public LayerMask platforms;

    public Sprite up, right, left;
    private bool isColliding;

    public int diamondAmount = 0;
    public int diamondsCollected;

    public Upgrades upgrades;

    [SerializeField] private AudioSource _fallingAudioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer(horizontalInput);
        if (!isColliding)
        {
            TiltPlayer(horizontalInput, tiltSpeed);
            HandleSpriteChange();
        }
    }

    private void OnEnable()
    {
        _fallingAudioSource.Play();
    }

    private void OnDisable()
    {
        _fallingAudioSource.Stop();
    }

    private void MovePlayer(float horizontalInput)
    {
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, horizontalInput * moveSpeed, lerpValue), 0);
        /* Day3 developer here
         * this is the old movement code, I prefer my solution 
         * but i still leave the original code here for you to choose the best one
         * 
         * Vector2 targetVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
         * rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, moveSpeed * Time.fixedDeltaTime);
        */
    }

    // Rotate the player based on his direction
    private void TiltPlayer(float horizontalInput,float tiltSpeed)
    {
        float targetTiltAngle = horizontalInput * tiltAngle;
        Quaternion targetTilt = Quaternion.Euler(0f, 0f, targetTiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTilt, tiltSpeed);
    }

    // Updates the sprite so it reflects the direction the player is going to
    private void HandleSpriteChange()
    {
        if (Mathf.Abs(transform.rotation.z) < 0.04f)
        {
            GetComponent<SpriteRenderer>().sprite = up;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = transform.rotation.z > 0 ? right : left;
        }
    }

    // if you collide stop updating the spirte until you are not colliding anymore
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        GetComponent<SpriteRenderer>().sprite = up;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}