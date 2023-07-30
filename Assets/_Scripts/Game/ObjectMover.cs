using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    // This value is very important, it stands for the speed at witch the object goes up the screen
    public float speed = 0;

    private void Update() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 15)
        {
            Destroy(gameObject);
        }
    }
}
