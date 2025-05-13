using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatReport // this class acts as a container for a single combat report
{
    public string npcName; // npcs name
    public int npcTier; // npcs tier
    public float damageDealt; // damage dealt by the npc
    public float parseTime; // time elapsed during the fight parse
    public bool dirtyParseTime; // if the time is set dirty, it means the npc died before end of combat and we must recalculate the time taken
    public Color unitColor; // units color used for the UI bars

    public CombatReport(string _npcName, int _npcTier, float _damageDealt, float _parseTime, bool _dirtyParseTime, Color _unitColor) // construct
    {
        this.npcName = _npcName;
        this.npcTier = _npcTier;
        this.damageDealt = _damageDealt;
        this.parseTime = _parseTime;
        this.dirtyParseTime = _dirtyParseTime;
        this.unitColor = _unitColor;
    }
}
