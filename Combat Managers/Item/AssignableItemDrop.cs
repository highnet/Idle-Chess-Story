using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssignableItemDrop : MonoBehaviour
{
    public Item item;
    public Outline outline;
    public BoardController boardController;
    public UiController uiController;
    public LineRenderer SelectionLine;
    public ArrowRenderer arrowRenderer;
    private Vector3 belowTheScreen;

    private void Start()
    {
        boardController = GetComponentInParent<BoardController>();
        uiController = GetComponentInParent<UiController>();
        SelectionLine = GetComponent<LineRenderer>();
        arrowRenderer = GetComponentInChildren<ArrowRenderer>();
        outline = GetComponent<Outline>();
        if (this.item.ItemRarity.Equals(ItemRarity.Trash))
        {
            this.outline.OutlineColor = Color.gray;
        }else if(this.item.ItemRarity.Equals(ItemRarity.Common))
        {
            this.outline.OutlineColor = Color.green;
        }
        else if (this.item.ItemRarity.Equals(ItemRarity.Rare))
        {
            this.outline.OutlineColor = Color.blue;
        }
        else if (this.item.ItemRarity.Equals(ItemRarity.Artifact))
        {
            this.outline.OutlineColor = new Color(190, 65, 0); //orange
        }
        this.outline.OutlineWidth = this.item.outlineWidth;

        belowTheScreen = new Vector3(0, -10, 0);
    }
    private void FixedUpdate()
    {
        if (boardController.selectedItemDrop == this)
        {
            arrowRenderer.enabled = true;
            Vector3[] positions = new Vector3[2];
            positions[0] = this.transform.position;

            if (boardController.mousedOverNPC != null)
            {
                positions[1] = boardController.mousedOverNPC.transform.position;
                arrowRenderer.SetPositions(this.transform.position, boardController.mousedOverNPC.transform.position);
            }
            else
            {
                positions[1] = this.transform.position;
                arrowRenderer.SetPositions(this.transform.position,this.transform.position);
            }

            SelectionLine.SetPositions(positions);
        }
        else
        {
            arrowRenderer.SetPositions(this.transform.position, this.transform.position);
        }
    }

    private void OnMouseOver()
    {
        boardController.FocusedItem = this.item;
        uiController.UpdateFocusedItemTooltip(this.item);
    }
    private void OnMouseExit()
    {
        boardController.FocusedItem = new Item(ItemName.NO_ITEM);
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
