using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatLogger : MonoBehaviour
{
    public Dictionary<string, float> combatReport;
    // Start is called before the first frame update
    void Start()
    {
        combatReport = new Dictionary<string, float>();
    }

    public void ResetCombatLog()
    {
        combatReport = new Dictionary<string, float>();
    }
}
