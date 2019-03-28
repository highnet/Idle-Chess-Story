using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{

    public GameObject emptyBar;
    public GameObject fullBar;
    public GameObject emptyConcentrationBar;
    public GameObject fullConcentrationBar;

    public GameObject tier1Marker;
    public GameObject tier2Marker;
    public GameObject tier3Marker;

    public bool isMaxRank;

    float fillPercentageHealth;
    float fillPercentageConcentration;


    NPC attachedNpc;

    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        isMaxRank = false;
        attachedNpc = GetComponentInParent<NPC>();
        mainCamera = GameObject.Find("Camera");

    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (!isMaxRank) {
            if (attachedNpc.TIER == 1)
            {
                tier1Marker.SetActive(true);
            } else if (attachedNpc.TIER == 2)
            {
                tier1Marker.SetActive(true);
                tier2Marker.SetActive(true);
            } else if (attachedNpc.TIER == 3)
            {
                tier1Marker.SetActive(true);
                tier2Marker.SetActive(true);
                tier3Marker.SetActive(true);
                isMaxRank = true;
            }
        }
      
        if (attachedNpc.MAXHP != 0)
        {
            fillPercentageHealth = (attachedNpc.HP / attachedNpc.MAXHP);
        }

        if (attachedNpc.MAXCONCENTRATION != 0)
        {
            fillPercentageConcentration = (attachedNpc.CONCENTRATION / attachedNpc.MAXCONCENTRATION);
        }
        transform.rotation = Camera.main.transform.rotation; // "billboard" the hp bar gui
        fullBar.transform.localScale = new Vector3(fillPercentageHealth,1,1);
        fullConcentrationBar.transform.localScale = new Vector3(fillPercentageConcentration, 1, 1);

    }
}
