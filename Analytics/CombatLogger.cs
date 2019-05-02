using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatLogger : MonoBehaviour
{
    public List<CombatReport> combatReports;
    public List<GameObject> combatReportBars;
    public float parseTime;

    // Start is called before the first frame update
    void Start()
    {
        ResetCombatReportBars();
        ResetCombatLog();
    }

    public void ResetCombatLog()
    {
        combatReports = new List<CombatReport>();
    }

    public void ResetCombatReportBars()
    {
        foreach(GameObject go in combatReportBars)
        {
            GameObject.Destroy(go);
        }
        combatReportBars = new List<GameObject>();
    }

    public void AddCombatReport(CombatReport _combatReport)
    {
        combatReports.Add(_combatReport);
    }
}
