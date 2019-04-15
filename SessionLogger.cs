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
    public float mmrChange = 0;

    private void Start()
    {
        ReinitializeTribesDeployedToFightTrackerDictionary();
    }
    public void calculateFIDEMMRChange(bool combatVictory, float player1Rating, float player2Rating, float FIDE_KFactor)
    {
        float player1transformed_rating = (float) Math.Pow(10, (player1Rating / 400));
        float player2transformed_rating = (float)Math.Pow(10, (player2Rating / 400));
        float player1ExpectedScore = player1transformed_rating / (player1transformed_rating + player2transformed_rating);
        
        if (combatVictory)
        {
            Debug.Log(".......");
            Debug.Log(FIDE_KFactor);
            Debug.Log(player1ExpectedScore);
            Debug.Log((FIDE_KFactor * (1 - player1ExpectedScore)));
            Debug.Log(".......");
            mmrChange += (FIDE_KFactor * (1 - player1ExpectedScore));
        
        } else
        {
            mmrChange +=  (FIDE_KFactor * (0 - player1ExpectedScore)); 

        }

  
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
    }

    public void CalculateMaxDeployedTribe()
    {
        mostDeployedTribe = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        mostDeployedTribeAmount = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
    }


}
