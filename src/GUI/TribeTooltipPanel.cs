using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class TribeTooltipPanel : MonoBehaviour
{
    public GameObject tooltipPanel;

    public void MouseEnterToggle()
    {
        tooltipPanel.GetComponent<Image>().enabled = true;
        tooltipPanel.GetComponentInChildren<Text>().enabled = true;
    }
    public void MouseExitToggle()
    {
        tooltipPanel.GetComponent<Image>().enabled = false;
        tooltipPanel.GetComponentInChildren<Text>().enabled = false;
    }
}
