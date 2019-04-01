using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReport : ScriptableObject
{

    public float primaryDamageDealt;
    public float lifeStealHeal;
    public float retaliationDamageRecieved;
    public NPC damageSourceNPC;
    public NPC damageReceiverNPC;
    public bool wasCriticalStrike;
    public bool wasMiss;
    public bool wasDampenedMiss;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
