using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionLogger : MonoBehaviour // this class acts as a logger of a single gameplay session. used for logging various gameplay stats.
{
    public long goldRewarded; // gold rewarded to the player so far
    public int unitsDeployedToFight;  // units deployed to fight by the player
    public Dictionary<Tribe, int> TribesDeployedToFightTracker; // dictionary tracking how many of each tribe he has deployed
    public Tribe mostDeployedTribe;  // most deployed tribe by the player
    public int mostDeployedTribeAmount;  // most deployed tribe by the player (amount)
    public float mmrChange = 0; // calculated mmr change to the user so far
    public int itemDropsEarned = 0; // how many item drops the player has earned so far

    private void Start()
    {
        ReinitializeTribesDeployedToFightTrackerDictionary(); // restart dictionary
    }
    public void CalculateFIDEMMRChange(bool combatVictory, float player1Rating, float player2Rating, float FIDE_KFactor) // calculates mmr change to the player according to the FIDE ELO Standards
    {
        float player1transformed_rating = (float) Math.Pow(10, (player1Rating / 400)); // calculate transformed rating
        float player2transformed_rating = (float)Math.Pow(10, (player2Rating / 400)); // calculate transformed rating
        float player1ExpectedScore = player1transformed_rating / (player1transformed_rating + player2transformed_rating); // calculate expected score
        
        if (combatVictory) // apply mmr change
        {

            mmrChange += (FIDE_KFactor * (1 - player1ExpectedScore)); // victory
        
        } else
        {
            mmrChange +=  (FIDE_KFactor * (0 - player1ExpectedScore)); //defeat

        }
    }

    public void ReinitializeTribesDeployedToFightTrackerDictionary()
    {
        TribesDeployedToFightTracker = new Dictionary<Tribe, int>(); // reset
        var TribesArray = Enum.GetValues(typeof(Tribe)); // convert tribe enum to an array
        for (int i = 0; i < TribesArray.Length; i++)
        {
            TribesDeployedToFightTracker.Add((Tribe)TribesArray.GetValue(i), 0); // set value 0 for all indices
        }
    }

    public void IncrementTribesDeployedToFightCounters(NPC npc)
    {
        Helper.Increment<Tribe>(TribesDeployedToFightTracker, npc.PRIMARYTRIBE); // increment specified tribe by 1 in the dictionary
        Helper.Increment<Tribe>(TribesDeployedToFightTracker, npc.SECONDARYTRIBE); // increment specified tribe by 1 in the dictionary
    }

    public void CalculateMaxDeployedTribe()
    {
        mostDeployedTribe = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Key; // get the most deployed tribe
        mostDeployedTribeAmount = TribesDeployedToFightTracker.Aggregate((l, r) => l.Value > r.Value ? l : r).Value; // get the most deployed tribe (amount)
    }
}
