using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour

{
    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;
    public AudioClip track4;
    public AudioClip track5;
    public AudioClip track6;
    public AudioClip track7;
    public AudioClip track8;
    public AudioClip track9;
    private Queue<AudioClip> bgMusic;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic = new Queue<AudioClip>();
        bgMusic.Enqueue(track1);
        bgMusic.Enqueue(track2);
        bgMusic.Enqueue(track3);
        bgMusic.Enqueue(track4);
        bgMusic.Enqueue(track5);
        bgMusic.Enqueue(track6);
        bgMusic.Enqueue(track7);
        bgMusic.Enqueue(track8);
        bgMusic.Enqueue(track9);
        PlayNextSong();
    }

    void PlayNextSong()
    {
        audioSource.clip = bgMusic.Dequeue();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && bgMusic.Count > 0)
        {
            PlayNextSong();
        }
    }
}
