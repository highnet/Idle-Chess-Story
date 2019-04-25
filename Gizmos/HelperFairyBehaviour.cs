using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFairyBehaviour : MonoBehaviour
{
    GameObject selectedObject;
    BoardController boardController;

    // Start is called before the first frame update

    private void Awake()
    {
        boardController = GameObject.FindGameObjectWithTag("Controller").GetComponent<BoardController>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
            selectedObject = boardController.selectedObject;
        if (selectedObject != null)
        {
            this.transform.position = selectedObject.transform.position;

        } else
        {
            this.transform.position = Vector3.up * 100;
        }

       
    }
}
