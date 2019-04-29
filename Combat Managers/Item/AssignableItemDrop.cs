using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssignableItemDrop : MonoBehaviour
{
    public Item Item;
    public Outline outline;
    public BoardController boardController;
    public LineRenderer SelectionLine;

    private void Start()
    {
        boardController = GetComponentInParent<BoardController>();
        SelectionLine = GetComponent<LineRenderer>();
        outline = GetComponent<Outline>();
        if (this.Item.ItemRarity.Equals(ItemRarity.Trash))
        {
            this.outline.OutlineColor = Color.gray;
        }else if(this.Item.ItemRarity.Equals(ItemRarity.Common))
        {
            this.outline.OutlineColor = Color.green;
        }
        else if (this.Item.ItemRarity.Equals(ItemRarity.Rare))
        {
            this.outline.OutlineColor = Color.blue;
        }
        else if (this.Item.ItemRarity.Equals(ItemRarity.Artifact))
        {
            this.outline.OutlineColor = new Color(190, 65, 0); //orange
        }
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
            }
            else
            {
                positions[1] = this.transform.position;
            }

            SelectionLine.SetPositions(positions);
        }
        else
        {
            SelectionLine.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log(this.Item.ItemName.ToString());
    }
    private void OnMouseUp()
    {
        if (boardController.gameStatus == GameStatus.Shopping)
        {
            boardController.selectedItemDrop = this;
            boardController.selectedNPC = null;
        }
    }
}
