using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatReport
{
    public string npcName;
    public int npcTier;
    public float damageDealt;
    public float parseTime;
    public bool dirtyParseTime;
    public Color unitColor;

    public CombatReport(string _npcName, int _npcTier, float _damageDealt, float _parseTime, bool _dirtyParseTime, Color _unitColor)
    {
        this.npcName = _npcName;
        this.npcTier = _npcTier;
        this.damageDealt = _damageDealt;
        this.parseTime = _parseTime;
        this.dirtyParseTime = _dirtyParseTime;
        this.unitColor = _unitColor;
    }
}
