using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Warning Object",menuName = "Custom Objects/Warning Object")]
public class WarningObject : ScriptableObject
{
    // super cool scriptable object created by the day3 developer
    public float spawnTime = 1f;
    public bool useWarning = true;
    public GameObject graphics;
    public AudioClip spawnsfx;
}
