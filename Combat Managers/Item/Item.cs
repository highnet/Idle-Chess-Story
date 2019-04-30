using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName {StickSword,TwoHander,DragonKnife,LifeStone,SkullShield,WoodenShield,ShortSword,FlamingScimitar,PoisonedRapier,LongSword,MoonKatana,BroadSword}
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
        else if (ItemToCreate == ItemName.TwoHander)
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
            this.MAXHP_Bonus += 1000;
        }
        else if (ItemToCreate == ItemName.SkullShield)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ARMOR_Bonus += 7;
        }
        else if (ItemToCreate == ItemName.WoodenShield)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ARMOR_Bonus += 3;
        }
        else if (ItemToCreate == ItemName.ShortSword)
        {
            this.ItemRarity = ItemRarity.Common;
            this.ATTACKPOWER_Bonus += 45;
        }
        else if (ItemToCreate == ItemName.FlamingScimitar)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 25;
            this.SPELLPOWER_Bonus += 50;
        }
        else if (ItemToCreate == ItemName.PoisonedRapier)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 50;
            this.SPELLPOWER_Bonus += 25;
        }
        else if (ItemToCreate == ItemName.LongSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ATTACKPOWER_Bonus += 30;
            this.RETALIATION_Bonus += 3;
        }
        else if (ItemToCreate == ItemName.MoonKatana)
        {
            this.ItemRarity = ItemRarity.Artifact;
            this.ATTACKPOWER_Bonus += 50;
            this.ARMOR_Bonus += 5;
            this.RETALIATION_Bonus += 5;
            this.SPELLPOWER_Bonus += 50;

        }
        else if (ItemToCreate == ItemName.BroadSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.RETALIATION_Bonus += 1;
            this.ATTACKPOWER_Bonus += 25;
        }

    }
}


