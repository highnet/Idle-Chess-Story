using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop : MonoBehaviour
{
    public Item ItemDroppedInChest;
    public BoardController boardController;
    public LineRenderer SelectionLine;

    private void Start()
    {
        boardController = GetComponentInParent<BoardController>();
        SelectionLine = GetComponent<LineRenderer>();
        ItemName itemName = (ItemName)Enum.GetValues(typeof(ItemName)).GetValue((int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemName)).Length));
        Debug.Log("rng item: " + itemName.ToString());
        Item rngItem = new Item(itemName);
        ItemDroppedInChest = rngItem; // get a random tribe); 
        Debug.Log("Generated Item: " + rngItem.ItemName.ToString() + " for Treasure Drop");
       
    }
    private void Update()
    {
        if (boardController.selectedItemDrop == this)
        {
            SelectionLine.enabled = true;
        Vector3[] positions = new Vector3[2];
        positions[0] = this.transform.position;

        if (boardController.mousedOverNPC != null)
        {
            positions[1] = boardController.mousedOverNPC.transform.position;
        } else
        {
            positions[1] = this.transform.position;
        }

        SelectionLine.SetPositions(positions);
        } else
        {
            SelectionLine.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log(this.ItemDroppedInChest.ItemName.ToString());
    }
    private void OnMouseUp()
    {
        if (boardController.gameStatus == GameStatus.Shopping)
        {
            boardController.selectedItemDrop = this;
            boardController.selectedNPC = null;
        }
       else
        {

            Vector3 lootDropForce = new Vector3(UnityEngine.Random.Range(-200, 201), UnityEngine.Random.Range(100, 201), UnityEngine.Random.Range(-200, 201));
            GetComponent<Rigidbody>().AddForce(lootDropForce);
        }

    }
}
