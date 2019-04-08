using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSideIdentifierCircleToggler : MonoBehaviour { 


      public GameObject greenCircle;
      public GameObject redCircle;

   public void ActivateGreenCircle() // toggle
    {
        greenCircle.SetActive(true);
        redCircle.SetActive(false);
    }


   public void ActivateRedCircle() // toggle
    {
        greenCircle.SetActive(false);
        redCircle.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, 10f * Time.deltaTime, 0f)); // rotate the circle
    }
}
