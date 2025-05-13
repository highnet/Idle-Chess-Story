using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemThumbnail : MonoBehaviour
{
    GameObject worldController;
    BoardController boardController;
    UiController uiController;
    public Item shownItem;
    public Image shownItemThumbnail;
    private void Start()
    {
        worldController = GameObject.Find(("World Controller"));
        boardController = worldController.GetComponent<BoardController>();
        uiController = worldController.GetComponent<UiController>();
        ResetInventorySlotView();
    }
    public void ResetInventorySlotView()
    {
        shownItem = new Item(ItemName.NO_ITEM);
    }
    public void SetImage()
    {
        shownItemThumbnail.sprite = shownItem.thumbnailSprite;
    }
    public void SetFocusedItem()
    {
        uiController.MousedOverSelectedItemTooltipPanel.SetActive(true);
        boardController.FocusedItem = shownItem;
        uiController.UpdateFocusedItemTooltip(shownItem);
 
    }
    public void ClearFocusedItem()
    {
        boardController.FocusedItem = new Item(ItemName.NO_ITEM);
    }
}
