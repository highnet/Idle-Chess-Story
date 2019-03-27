using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSideIdentifierCircleToggler : MonoBehaviour { 


      public GameObject greenCircle;
      public GameObject redCircle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void ActivateGreenCircle()
    {
        greenCircle.SetActive(true);
        redCircle.SetActive(false);
    }


   public void ActivateRedCircle()
    {
        greenCircle.SetActive(false);
        redCircle.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0f, 10f * Time.deltaTime, 0f));
    }
}
