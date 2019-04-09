using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionLogger : MonoBehaviour
{
    public long goldRewarded;
    public int unitsDeployedToFight; 
    public Tribe mostDeployedTribe; 
    public int mostDeployedTribeAmount; 
    public Dictionary<Tribe, int> TribesDeployedToFightTracker;

    private void Start()
    {
        ReinitializeTribesDeployedToFightTrackerDictionary();
    }
    
    public void ReinitializeTribesDeployedToFightTrackerDictionary()
    {
        TribesDeployedToFightTracker = new Dictionary<Tribe, int>();
        var TribesArray = Enum.GetValues(typeof(Tribe));
        for (int i = 0; i < TribesArray.Length; i++)
        {
            TribesDeployedToFightTracker.Add((Tribe)TribesArray.GetValue(i), 0);
        }
    }

    public void IncrementTribesDeployedToFightCounters(NPC npc)
    {
        Helper.Increment<Tribe>(TribesDeployedToFightTracker, npc.PRIMARYTRIBE);
        Helper.Increment<Tribe>(TribesDeployedToFightTracker, npc.SECONDARYTRIBE);
        Debug.Log(string.Join(", ", (TribesDeployedToFightTracker.Select(pair => $"{pair.Key} => {pair.Value}"))));

    }

    public void CalculateMaxDeployedTribe()
    {
        mostDeployedTribe = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        mostDeployedTribeAmount = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
    }


}
