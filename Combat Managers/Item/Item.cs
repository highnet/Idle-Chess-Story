using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName {ShortSword, ShortStaff,RoundShield,LifeRune,ChainMail}
// public enum ItemRarity {Trash, Normal, Rare, Artifact}
[System.Serializable]
public class Item 
{
    public ItemName ItemName;
    public int MAXHP_Bonus;
    public int ARMOR_Bonus;
    public int ATTACKPOWER_Bonus;
    public int SPELLPOWER_Bonus;
    public int RETALIATION_Bonus;

    public Item(ItemName ItemToCreate)
    {
        Debug.Log("RECIEVED ITEMNAME :" + ItemToCreate.ToString());
        this.ItemName = ItemToCreate;
        if (ItemToCreate == ItemName.ShortSword)
        {
            this.ATTACKPOWER_Bonus += 50;
        } else if(ItemToCreate == ItemName.ShortStaff)
        {
            this.SPELLPOWER_Bonus += 50;
        }
        else if (ItemToCreate == ItemName.RoundShield)
        {
            this.ARMOR_Bonus += 5;
        }
        else if (ItemToCreate == ItemName.LifeRune)
        {
            this.MAXHP_Bonus += 250;
        }
        else if (ItemToCreate == ItemName.ChainMail)
        {
            this.ARMOR_Bonus += 3;
        }

    }
}


