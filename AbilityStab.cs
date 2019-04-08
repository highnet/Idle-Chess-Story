using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStab : MonoBehaviour
{

    public AudioClip soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(soundToPlay, this.transform.position);
    }

}
