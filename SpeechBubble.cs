using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{


    private void OnMouseDown()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation; // "billboard"
    }
}
