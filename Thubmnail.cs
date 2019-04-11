﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Thubmnail : MonoBehaviour
{
    public GameObject SourcePrefab;
    public GameObject SpawnedAssociatedNPC;
    public UiController uicontroller;
    public int randomRotationOrientation;
    public float shopThumbnailRotationSpeed = 5f;
    private bool mousedOver = false;
    private int xOffset = 0;
    private int yOffset = -Screen.width / 2;
    private Vector3 offsetVector;

    private void Start()
    {
        
        uicontroller = GameObject.Find("World Controller").GetComponent<UiController>();
        offsetVector = new Vector3(xOffset, yOffset, 0);
        randomRotationOrientation = UnityEngine.Random.Range(-1, 1);
        if (randomRotationOrientation == 0)
        {
            randomRotationOrientation = 1;
        }
    }

    private void LateUpdate()
    {
        if (mousedOver)
        {
            SpawnedAssociatedNPC.transform.Rotate(new Vector3(0f, randomRotationOrientation * shopThumbnailRotationSpeed, 0f), Space.Self);
            RectTransform rectTrans = uicontroller.ShopPanelTooltipSubPanel.GetComponent<RectTransform>();
         rectTrans.anchoredPosition = Input.mousePosition + offsetVector;
        }
    }

    public void MouseOver()
    {
        uicontroller.ShopPanelTooltipSubPanel.gameObject.SetActive(true);
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.gameObject.SetActive(true);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.gameObject.SetActive(true);
        uicontroller.shopMouseOverInfoText_NAME.text = SourcePrefab.GetComponentInChildren<Prefab_Thumbnail>().unit.ToString();
        uicontroller.shopMouseOverInfoText_PRIMARYTRIBE.text = SourcePrefab.GetComponentInChildren<Prefab_Thumbnail>().primaryTribe.ToString();
        uicontroller.shopMouseOverInfoText_SECONDARYTRIBE.text = SourcePrefab.GetComponentInChildren<Prefab_Thumbnail>().secondaryTribe.ToString();
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.SetImage(SourcePrefab.GetComponentInChildren<Prefab_Thumbnail>().primaryTribe, true);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.SetImage(SourcePrefab.GetComponentInChildren<Prefab_Thumbnail>().secondaryTribe, true);
        mousedOver = true;

    }
    public void MouseExit()
    {

        uicontroller.shopMouseOverInfoText_NAME.text = "";
        uicontroller.shopMouseOverInfoText_PRIMARYTRIBE.text = "";
        uicontroller.shopMouseOverInfoText_SECONDARYTRIBE.text = "";
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.gameObject.SetActive(false);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.gameObject.SetActive(false);
        uicontroller.ShopPanelTooltipSubPanel.gameObject.SetActive(false);
        SpawnedAssociatedNPC.transform.rotation = Quaternion.identity;
        SpawnedAssociatedNPC.transform.Rotate(0f, 180f, 0f, Space.Self);
        mousedOver = false;

    }
}
