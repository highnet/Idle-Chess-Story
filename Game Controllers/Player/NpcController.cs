using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcController : MonoBehaviour
{
    public List<NPC> npcList;
    public List<NPC> enemyList;
    public List<NPC> allyList;
    public List<NPC> allyListBackup;
    public List<NPC> deployedAllyList;

    // Use this for initialization
    void Start()
    {
        UpdateNpcList();

        allyListBackup = new List<NPC>();
        deployedAllyList = new List<NPC>();
    }


    // Update is called once per frame
    void Update() { }

    public void UpdateNpcList()
    {
        npcList = new List<NPC>();
        enemyList = new List<NPC>();
        allyList = new List<NPC>();

        

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Targetable"))
        {
            npcList.Add(obj.GetComponent<NPC>());

            if (obj.GetComponent<NPC>().isEnemy)
            {
                enemyList.Add(obj.GetComponent<NPC>());
            }
            else
            {
                allyList.Add(obj.GetComponent<NPC>());
            }
        }

    }
}
