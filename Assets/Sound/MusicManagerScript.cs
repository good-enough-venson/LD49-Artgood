using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public AudioSource soundTrack;
    public float loopDelay = 6;
    bool playing = false;
    float playTime = 10;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (playing & !soundTrack.isPlaying) {
            playTime = Time.time + loopDelay;
        }

        playing = soundTrack.isPlaying;

        if (!playing && Time.time > playTime) {
            soundTrack.Play();
        }
    }
}
