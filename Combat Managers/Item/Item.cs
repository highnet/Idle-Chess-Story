using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName {StickSword,TwoHander,DragonKnife,LifeStone,SkullShield,WoodenShield,ShortSword,FlamingScimitar,PoisonedRapier,LongSword,MoonKatana,BroadSword,NO_ITEM}
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
    public string Tooltip;
    public float outlineWidth;
    public Sprite thumbnailSprite;

    public Item(ItemName ItemToCreate)
    {
        this.ItemName = ItemToCreate;
        if (ItemToCreate == ItemName.StickSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ATTACKPOWER_Bonus += 25;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.TwoHander)
        {
            this.ItemRarity = ItemRarity.Common;
            this.ATTACKPOWER_Bonus += 45;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.DragonKnife)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 65;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.LifeStone)
        {
            this.ItemRarity = ItemRarity.Artifact;
            this.MAXHP_Bonus += 1000;
            outlineWidth = 0.5f;
        }
        else if (ItemToCreate == ItemName.SkullShield)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ARMOR_Bonus += 7;
            this.RETALIATION_Bonus += 3;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.WoodenShield)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ARMOR_Bonus += 3;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.ShortSword)
        {
            this.ItemRarity = ItemRarity.Common;
            this.ATTACKPOWER_Bonus += 45;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.FlamingScimitar)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 25;
            this.SPELLPOWER_Bonus += 50;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.PoisonedRapier)
        {
            this.ItemRarity = ItemRarity.Rare;
            this.ATTACKPOWER_Bonus += 50;
            this.SPELLPOWER_Bonus += 25;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.LongSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ATTACKPOWER_Bonus += 30;
            this.RETALIATION_Bonus += 3;
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.MoonKatana)
        {
            this.ItemRarity = ItemRarity.Artifact;
            this.ATTACKPOWER_Bonus += 50;
            this.ARMOR_Bonus += 5;
            this.RETALIATION_Bonus += 5;
            this.SPELLPOWER_Bonus += 50;
            outlineWidth = 0.5f;
        }
        else if (ItemToCreate == ItemName.BroadSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.RETALIATION_Bonus += 1;
            this.ATTACKPOWER_Bonus += 25;
        }

        int ap = ATTACKPOWER_Bonus;
        int sp = SPELLPOWER_Bonus;
        int armor = ARMOR_Bonus;
        int retaliation = RETALIATION_Bonus;
        int maxhp = MAXHP_Bonus;


        List<string> nonZeroStats = new List<string>();
        if (ap != 0)
        {
            nonZeroStats.Add("AP: " + ap.ToString());
        }
        if (sp != 0)
        {
            nonZeroStats.Add("SP: " + ap.ToString());
        }
        if (armor != 0)
        {
            nonZeroStats.Add("Armor: " + armor.ToString());
        }
        if (retaliation != 0)
        {
            nonZeroStats.Add("Retaliation: " + retaliation.ToString());
        }
        if (maxhp != 0)
        {
            nonZeroStats.Add("HP: " + maxhp.ToString());
        }

        bool firstLine = true;
        foreach(string line in nonZeroStats)
        {
            if (firstLine)
            {
                Tooltip += line;
                firstLine = false;
            } else
            {
                Tooltip += "\n" + line;
            }
        }
        this.thumbnailSprite = Resources.Load<Sprite>(ItemName.ToString() + "_thumbnail");

    }
}


