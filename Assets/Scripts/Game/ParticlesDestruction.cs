using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestruction : MonoBehaviour
{
    float timeLeft = 3.5f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            Destroy(this.gameObject);
        } 
    }
}
