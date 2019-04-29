using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop : MonoBehaviour
{
    public Item ItemDroppedInChest;
    public BoardController boardController;
    public PlayerController playerController;

    private void Start()
    {
        boardController = GetComponentInParent<BoardController>();
        playerController = GetComponentInParent<PlayerController>();
        int RarityRoll = UnityEngine.Random.Range(0, 101);
        ItemRarity rolledRarity = ItemRarity.Trash;
        if (RarityRoll >= 60 && RarityRoll < 90)
        {
            rolledRarity = ItemRarity.Common;
        }
        else if (RarityRoll >= 90 && RarityRoll < 99)
        {
            rolledRarity = ItemRarity.Rare;
        }
        else if (RarityRoll >= 99)
        {
            rolledRarity = ItemRarity.Artifact;
        }
        Debug.Log("Rolled: " + RarityRoll + " and getting a random item of rarity: " + rolledRarity.ToString());
        List<ItemName> possibleDrops;
        playerController.ITEM_RARITY_DATA.TryGetValue(rolledRarity, out possibleDrops); // get a list of all units of our possible item drops
        Debug.Log("Possible drops count: " + possibleDrops.Count);
        int possibleDropIndex = UnityEngine.Random.Range(0, possibleDrops.Count);
        ItemName itemName = possibleDrops[possibleDropIndex];
        Item rngItem = new Item(itemName);
        ItemDroppedInChest = rngItem; 
        Debug.Log("Generated Item: " + rngItem.ItemName.ToString() + " for Treasure Drop");
       
    }

    private void OnMouseOver()
    {
        Debug.Log(this.ItemDroppedInChest.ItemName.ToString());
    }
    private void OnMouseUp()
    {
        if (boardController.gameStatus == GameStatus.Shopping)
        {
            GameObject Loot = (GameObject)Instantiate(Resources.Load(ItemDroppedInChest.ItemName.ToString()), boardController.transform);
            Loot.GetComponent<AssignableItemDrop>().Item = ItemDroppedInChest;
            Loot.transform.position = this.transform.position + (2 * Vector3.up);
            boardController.selectedItemDrop = Loot.GetComponent<AssignableItemDrop>();
            Vector3 lootDropForce = new Vector3(UnityEngine.Random.Range(-20, 21), UnityEngine.Random.Range(10, 21), UnityEngine.Random.Range(-20, 21));
            Loot.GetComponent<Rigidbody>().AddForce(lootDropForce);
            Vector3 lootDropRotation = new Vector3(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));
            Loot.GetComponent<Rigidbody>().AddTorque(lootDropRotation);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            Vector3 lootDropForce = new Vector3(UnityEngine.Random.Range(-200, 201), UnityEngine.Random.Range(100, 201), UnityEngine.Random.Range(-200, 201));
            GetComponent<Rigidbody>().AddForce(lootDropForce);
            Vector3 lootDropRotation = new Vector3(UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361));
            GetComponent<Rigidbody>().AddTorque(lootDropRotation);
        }

    }
}
