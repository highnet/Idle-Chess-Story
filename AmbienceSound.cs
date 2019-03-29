using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RNGHelper
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class AmbienceSound : MonoBehaviour
{

    public AudioClip ambience1;
    public AudioClip ambience2;
    public AudioClip ambience3;
    public AudioClip ambience4;
    public AudioClip ambience5;

    private IList<AudioClip> ambienceMusic;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ambienceMusic = new List<AudioClip>();
        ambienceMusic.Add(ambience1);
        ambienceMusic.Add(ambience2);
        ambienceMusic.Add(ambience3);
        ambienceMusic.Add(ambience4);
        ambienceMusic.Add(ambience5);
        RNGHelper.Shuffle<AudioClip>(ambienceMusic);
        PlayNextSong();
    }

    void PlayNextSong()
    {
        audioSource.clip = ambienceMusic[0];
        ambienceMusic.RemoveAt(0);
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && ambienceMusic.Count > 0)
        {
            PlayNextSong();
        }
    }
}
