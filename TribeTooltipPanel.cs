using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class TribeTooltipPanel : MonoBehaviour
{

    public GameObject tooltipPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        
        tooltipPanel.GetComponent<Image>().enabled = true;
        tooltipPanel.GetComponentInChildren<Text>().enabled = true;
    }
    private void OnMouseExit()
    {
        tooltipPanel.GetComponent<Image>().enabled = false;
        tooltipPanel.GetComponentInChildren<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
