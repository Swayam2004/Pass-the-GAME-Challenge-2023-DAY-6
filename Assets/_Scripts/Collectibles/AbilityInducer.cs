using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityInducer : MonoBehaviour
{
    /* Hey! Day3 developer here, no one used this code
     * feel free to decide whether or not to use it or use the code that i have written as an
     * alternative solution :D
     */

    [Tooltip("This list of things will happen on picking up (very comfortable to work with)")]
    public UnityEvent OnPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnPickUp.Invoke();
        }
    }
}
