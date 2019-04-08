using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSave
{
    public string Name;
    public int Mmr;

    public PlayerSave(PlayerController player)
    {
        Name = player.playerName;
        Mmr = player.playerMMR;
    }
}
