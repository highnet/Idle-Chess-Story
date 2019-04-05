using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Thumbnail : MonoBehaviour
{

    private int randomRotationOrientation;
    public float shopThumbnailRotationSpeed = 2f;
    public Unit unit;
    public Tribe primaryTribe;
    public Tribe secondaryTribe;
    public UiController uicontroller;


    // Start is called before the first frame update
    void Start()
    {
        uicontroller = GameObject.Find("World Controller").GetComponent<UiController>();
        randomRotationOrientation = UnityEngine.Random.Range(-1, 1);
        if (randomRotationOrientation == 0)
        {
            randomRotationOrientation = 1;
        }
   
    }

    private void OnMouseOver()
    {
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.gameObject.SetActive(true);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.gameObject.SetActive(true);
 
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.SetImage(this.primaryTribe,true);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.SetImage(this.secondaryTribe,true);
  
        uicontroller.shopMouseOverInfoText_NAME.text = this.unit.ToString();
        uicontroller.shopMouseOverInfoText_PRIMARYTRIBE.text = this.primaryTribe.ToString();
        uicontroller.shopMouseOverInfoText_SECONDARYTRIBE.text = this.secondaryTribe.ToString();

        this.transform.Rotate(new Vector3(0f,randomRotationOrientation * shopThumbnailRotationSpeed, 0f),Space.Self);
    }
    private void OnMouseExit()
    {
        uicontroller.shopMouseOverInfoText_NAME.text = "";
        uicontroller.shopMouseOverInfoText_PRIMARYTRIBE.text = "";
        uicontroller.shopMouseOverInfoText_SECONDARYTRIBE.text = "";
        uicontroller.shopMouseOverInfoIcon_PRIMARYTRIBE_tribeIconVisualizer.gameObject.SetActive(false);
        uicontroller.shopMouseOverInfoIcon_SECONDARYTRIBE_tribeIconVisualizer.gameObject.SetActive(false);
        this.transform.rotation = Camera.main.transform.rotation;
        this.transform.Rotate(0f, 180f, 0f, Space.Self);
    }
}
