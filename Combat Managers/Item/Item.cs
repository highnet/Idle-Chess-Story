using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName {StickSword,MetalSword,DragonKnife,LifeStone}
public enum ItemRarity {Trash, Common, Rare, Artifact}
[System.Serializable]
public class Item 
{
    public ItemName ItemName;
    public ItemRarity ItemRarity;
    public int MAXHP_Bonus;
    public int ARMOR_Bonus;
    public int ATTACKPOWER_Bonus;
    public int SPELLPOWER_Bonus;
    public int RETALIATION_Bonus;

    public Item(ItemName ItemToCreate)
    {
        Debug.Log("RECIEVED ITEMNAME :" + ItemToCreate.ToString());
        this.ItemName = ItemToCreate;
        if (ItemToCreate == ItemName.StickSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ATTACKPOWER_Bonus += 25;
        }
        else if (ItemToCreate == ItemName.MetalSword)
        {
            this.ItemRarity = ItemRarity.Common;
            this.ATTACKPOWER_Bonus += 45;
        }
        else if (ItemToCreate == ItemName.DragonKnife)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 65;
        }
        else if (ItemToCreate == ItemName.LifeStone)
        {
            this.ItemRarity = ItemRarity.Artifact;
            this.MAXHP_Bonus += 300;
        }

    }
}


