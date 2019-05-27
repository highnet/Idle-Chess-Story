using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName {StickSword,TwoHander,DragonKnife,LifeStone,SkullShield,WoodenShield,ShortSword,FlamingScimitar,PoisonedRapier,LongSword,MoonKatana,BroadSword,NO_ITEM,WolfMask}
public enum ItemRarity {Trash, Common, Rare, Artifact}
public enum Stat {AP,SP,ARMOR,RETALIATION,MAXHP}
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
    GameObject worldController;
    UiController uiController;
    Translator translator;


    public Item(ItemName ItemToCreate)
    {
        worldController = GameObject.Find("World Controller");
        uiController = worldController.GetComponent<UiController>();
        translator = worldController.GetComponent<Translator>();

        this.ItemName = ItemToCreate;
        if (ItemToCreate == ItemName.StickSword)
        {
            this.ItemRarity = ItemRarity.Trash;
            this.ATTACKPOWER_Bonus += 35;
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
            this.ATTACKPOWER_Bonus += 15;
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
            outlineWidth = 1;
        }
        else if (ItemToCreate == ItemName.WolfMask)
        {
            this.ItemRarity = ItemRarity.Common;
            this.RETALIATION_Bonus += 2;
            this.ATTACKPOWER_Bonus += 25;
            this.MAXHP_Bonus += 100;
            outlineWidth = 0.7f;
        }

        int ap = ATTACKPOWER_Bonus;
        int sp = SPELLPOWER_Bonus;
        int armor = ARMOR_Bonus;
        int retaliation = RETALIATION_Bonus;
        int maxhp = MAXHP_Bonus;


        List<string> nonZeroStats = new List<string>();

        if (ap != 0)
        {
            nonZeroStats.Add(translator.TranslateStat(Stat.AP,uiController.currentLanguage) + "+" + ap.ToString());
        }
        if (sp != 0)
        {
            nonZeroStats.Add(translator.TranslateStat(Stat.SP, uiController.currentLanguage) + "+" + sp.ToString());
        }
        if (armor != 0)
        {
            nonZeroStats.Add(translator.TranslateStat(Stat.ARMOR, uiController.currentLanguage) + "+" + armor.ToString());
        }
        if (retaliation != 0)
        {
            nonZeroStats.Add(translator.TranslateStat(Stat.RETALIATION, uiController.currentLanguage) + "+" + retaliation.ToString());
        }
        if (maxhp != 0)
        {
            nonZeroStats.Add(translator.TranslateStat(Stat.MAXHP, uiController.currentLanguage) + "+" + maxhp.ToString());
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


