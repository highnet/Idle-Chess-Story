using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbienceSound : MonoBehaviour
{

    public AudioClip rainLoop;
    public AudioSource audioSource;

    public void PlayRainLoop()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(rainLoop);
    }
    public void StopPlaying()
    {
        audioSource.Stop();
    }


}
