using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatLogger : MonoBehaviour // this class acts as a dps recount meter for the combat log
{
    public List<CombatReport> combatReports; // contains a list of combat reports
    public List<GameObject> combatReportBars; // containts a list of combat report UI bars
    public float parseTime; // time taken of the parse

    // Start is called before the first frame update
    void Start()
    {
        ResetCombatReportBars(); // reset
        ResetCombatLog(); // reset
    }

    public void ResetCombatLog()
    {
        combatReports = new List<CombatReport>(); // reset combat reports
    }

    public void ResetCombatReportBars()
    {
        foreach(GameObject go in combatReportBars)
        {
            GameObject.Destroy(go); //reset combat bars
        }
        combatReportBars = new List<GameObject>(); // instantiate new list
    }

    public void AddCombatReport(CombatReport _combatReport)
    {
        combatReports.Add(_combatReport); // add a combat report to the list
    }
}
