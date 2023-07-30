using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    // value changed by the gamemanager by the increase of the game time
    public float scrollSpeed = 0;

    private void Update()
    {
        // translate the background
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (transform.position.y > 9.83)
        {
            transform.position = new Vector2(transform.position.x, -9);
        }
    }
}
