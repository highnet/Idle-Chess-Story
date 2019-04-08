using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChessBoardBehaviour : MonoBehaviour
{
    BoardController boardController;

    private void Start()
    {
        boardController = GameObject.Find("World Controller").GetComponent<BoardController>();
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject(-1) && Input.GetMouseButtonDown(0))
        {

            boardController.selectedObject = null;
        }
    }
}
