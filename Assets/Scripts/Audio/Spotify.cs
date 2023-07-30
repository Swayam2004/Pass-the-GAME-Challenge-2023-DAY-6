using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotify : MonoBehaviour
{
    [SerializeField] AudioClip[] tracks;
    // enable if you want to play trcks randomly, disable if you want them in order
    [SerializeField] bool shuffle;
    AudioSource source;
    AudioClip currentClip;
    AudioClip nextClip;
    bool nextTrackSelected = false;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        if(GameObject.FindGameObjectsWithTag("Music").Length == 2) {
            if(gameObject.scene.buildIndex == -1) Destroy(gameObject);
        }
    }
    private void Update()
    {
        // if there are no tracks stop
        if (tracks == null) return;
        // play tracks, select a new track when fishied to play the previuous one
        if (!source.isPlaying)
        {
            source.PlayOneShot(currentClip);
            Debug.Log("Currently playing track: " + currentClip.name);
            nextTrackSelected = false;
        }
        else
        {
            if (!nextTrackSelected)
            {
                SelectNextTrack();
                Debug.Log("Next selected track: " + nextClip.name);
                nextTrackSelected = true;
            }
        }

    }

    private void SelectNextTrack()
    {
        if (shuffle)
        {
            // select a random track
            do
            {
                nextClip = tracks[UnityEngine.Random.Range(0, tracks.Length)];
            } while (nextClip == currentClip);
        }
        else
        {
            // select the next track
            int songindex = Array.IndexOf(tracks, currentClip);
            int nextsongindex;
            if(songindex == tracks.Length - 1)
            {
                nextsongindex = 0;
            }
            else
            {
                nextsongindex = songindex + 1;
            }
            nextClip = tracks[nextsongindex];
        }
        currentClip = nextClip;
    }
}
