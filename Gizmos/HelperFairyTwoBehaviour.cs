using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFairyTwoBehaviour : MonoBehaviour
{
    GameObject selectedObject;
    BoardController boardController;

    // Start is called before the first frame update

    private void Awake()
    {
        boardController = GameObject.FindGameObjectWithTag("Controller").GetComponent<BoardController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boardController.selectedItemDrop != null)
        {
                selectedObject = boardController.selectedItemDrop.gameObject;
                this.transform.position = selectedObject.transform.position;
        }
        else
        {
            this.transform.position = Vector3.up * 100;
        }


    }
}
