using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievements : MonoBehaviour
{
    // Our GameID
    private CGameID m_GameID;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;

    // Persisted Stat details
    private int m_GamesWon;
    private int m_RoundsWon;
    private int m_GoldEarned;
    private int m_BeastsDeployed;
    private int m_WarriorsDeployed;
    private int m_ElementalsDeployed;
    private int m_WizardsDeployed;
    private int m_AssassinsDeployed;
    private int m_UndeadsDeployed;
    private int m_StructuresDeployed;
    private int m_GuardiansDeployed;

    // are our stats valid?
    private bool m_StatsValid;

    void Start()
    {
        FindUserStats();
    }
    public void FindUserStats()
    {
        SteamUserStats.RequestCurrentStats();

    }

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
            m_GameID = new CGameID(SteamUtils.GetAppID()); 	// Cache the GameID for use in the Callbacks

        }
    }

    public void RewardBossKill(int bossNumberID)
    {
        if (m_StatsValid)
        {
            Debug.Log("[SteamStats] Rewarding Boss Kill (id: " + bossNumberID + ")");

            if (bossNumberID == 1)
            {
                SteamUserStats.SetStat("Eyebat Killed", 1);
            }
            else if (bossNumberID == 2)
            {
                SteamUserStats.SetStat("Alien Soldier Killed", 1);
            }
            else if (bossNumberID == 3)
            {
                SteamUserStats.SetStat("Engineer Killed", 1);
            }
            SteamUserStats.StoreStats();
        }

    }

    public void IncrementDeployedTribesStats(Dictionary<Tribe, int> TribesDeployedToFightTracker)
    {
        int beastsCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Beast, out beastsCount);

        int warriorsCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Warrior, out warriorsCount);

        int elementalsCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Elemental, out elementalsCount);

        int wizardsCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Wizard, out wizardsCount);

        int assassinsCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Assassin, out assassinsCount);

        int undeadCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Undead, out undeadCount);

        int structureCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Structure, out structureCount);

        int guardiansCount = 0;
        TribesDeployedToFightTracker.TryGetValue(Tribe.Guardian, out guardiansCount);

        if (m_StatsValid)
        {
            m_BeastsDeployed += beastsCount;
            SteamUserStats.SetStat("Beasts Deployed", m_BeastsDeployed);
            if (m_BeastsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Beasts Deployed", (uint)m_BeastsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_BeastsDeployed + " Beasts Deployed");

            m_WarriorsDeployed += warriorsCount;
            SteamUserStats.SetStat("Warriors Deployed", m_WarriorsDeployed);
            if (m_WarriorsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Warriors Deployed", (uint)m_BeastsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_WarriorsDeployed + " Warriors Deployed");

            m_ElementalsDeployed += elementalsCount;
            SteamUserStats.SetStat("Elementals Deployed", m_ElementalsDeployed);
            if (m_ElementalsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Elementals Deployed", (uint)m_ElementalsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_ElementalsDeployed + " Elementals Deployed");

            m_WizardsDeployed += wizardsCount;
            SteamUserStats.SetStat("Wizards Deployed", m_WizardsDeployed);
            if (m_WizardsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Wizards Deployed", (uint)m_WizardsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_WizardsDeployed + " Wizards Deployed");

            m_AssassinsDeployed += assassinsCount;
            SteamUserStats.SetStat("Assassins Deployed", m_AssassinsDeployed);
            if (m_AssassinsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Assassins Deployed", (uint)m_AssassinsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_AssassinsDeployed + " Assassins Deployed");

            m_UndeadsDeployed += undeadCount;
            SteamUserStats.SetStat("Undeads Deployed", m_UndeadsDeployed);
            if (m_UndeadsDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Undeads Deployed", (uint)m_UndeadsDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_UndeadsDeployed + " Undeads Deployed");

            m_StructuresDeployed += structureCount;
            SteamUserStats.SetStat("Structures Deployed", m_StructuresDeployed);
            if (m_StructuresDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Structures Deployed", (uint)m_StructuresDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_StructuresDeployed + " Structures Deployed");

            m_GuardiansDeployed += guardiansCount;
            SteamUserStats.SetStat("Guardians Deployed", m_GuardiansDeployed);
            if (m_GuardiansDeployed >= 250)
            {
                SteamUserStats.IndicateAchievementProgress("Guardians Deployed", (uint)m_GuardiansDeployed, 500);
            }
            Debug.Log("[SteamStats] " + m_GuardiansDeployed + " Guardians Deployed");

            SteamUserStats.StoreStats();
        }
    }

    public void IncrementGoldEarnedStats(long goldEarned)
    {
        if (m_StatsValid)
        {
            m_GoldEarned += (int)goldEarned;
            SteamUserStats.SetStat("Games Won", m_GoldEarned);
            if (m_GoldEarned < 10000)
            {
                SteamUserStats.IndicateAchievementProgress("Gold Earned", (uint)m_GoldEarned, 10000);
            }
        }
        SteamUserStats.StoreStats();
        Debug.Log("[SteamStats] " + m_GoldEarned + "Gold Earned");
    }

    public void IncrementGamesWon()
    {

        if (m_StatsValid)
        {
            SteamUserStats.SetStat("Games Won", ++m_GamesWon);
            if (m_GamesWon == 5)
            {
                SteamUserStats.IndicateAchievementProgress("Rookie", 5, 10);
            }
        }
        SteamUserStats.StoreStats();
        Debug.Log("[SteamStats] " + m_GamesWon + "Games Won");
    }

    public void IncrementRoundsWon(int roundsWon)
    {
        if (m_StatsValid)
        {
            m_RoundsWon += roundsWon;
            SteamUserStats.SetStat("Rounds Won", m_RoundsWon);
            if (m_RoundsWon == 5)
            {
                SteamUserStats.IndicateAchievementProgress("Pawn", 5, 10);
            }
            else if (m_RoundsWon == 13)
            {
                SteamUserStats.IndicateAchievementProgress("Knight", 13, 25);
            }
            else if (m_RoundsWon == 38)
            {
                SteamUserStats.IndicateAchievementProgress("Bishop", 38, 75);
            }
            else if (m_RoundsWon == 75)
            {
                SteamUserStats.IndicateAchievementProgress("Rook", 75, 150);
            }
            else if (m_RoundsWon == 150)
            {
                SteamUserStats.IndicateAchievementProgress("Queen", 150, 300);
            }
            else if (m_RoundsWon == 250)
            {
                SteamUserStats.IndicateAchievementProgress("King", 250, 500);
            }
        }
        SteamUserStats.StoreStats();
        Debug.Log("[SteamStats] " + m_RoundsWon + "Rounds Won");
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                // load achievements
                //   SteamUserStats.GetAchievement("Victory",out m_VictoryAchieved);
                // load stats
                SteamUserStats.GetStat("Games Won", out m_GamesWon);
                SteamUserStats.GetStat("Rounds Won", out m_RoundsWon);
                SteamUserStats.GetStat("Gold Earned", out m_GoldEarned);
                SteamUserStats.GetStat("Warriors Deployed", out m_WarriorsDeployed);
                SteamUserStats.GetStat("Wizards Deployed", out m_WizardsDeployed);
                SteamUserStats.GetStat("Beasts Deployed", out m_BeastsDeployed);
                SteamUserStats.GetStat("Undeads Deployed", out m_UndeadsDeployed);
                SteamUserStats.GetStat("Elementals Deployed", out m_ElementalsDeployed);
                SteamUserStats.GetStat("Structures Deployed", out m_StructuresDeployed);
                SteamUserStats.GetStat("Guardians Deployed", out m_GuardiansDeployed);
                SteamUserStats.GetStat("Assassins Deployed", out m_AssassinsDeployed);

                m_StatsValid = true;
            }
            else
            {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }

            Debug.Log("[SteamStats] " + m_GamesWon + " Games Won");
            Debug.Log("[SteamStats] " + m_RoundsWon + " Rounds Won");
            Debug.Log("[SteamStats] " + m_GoldEarned + " Gold Earned");
            Debug.Log("[SteamStats] " + m_WarriorsDeployed + " Warriors Deployed");
            Debug.Log("[SteamStats] " + m_WizardsDeployed + " Wizards Deployed");
            Debug.Log("[SteamStats] " + m_BeastsDeployed + " Beasts Deployed");
            Debug.Log("[SteamStats] " + m_UndeadsDeployed + " Undeads Deployed");
            Debug.Log("[SteamStats] " + m_ElementalsDeployed + " Elementals Deployed");
            Debug.Log("[SteamStats] " + m_StructuresDeployed + " Structures Deployed");
            Debug.Log("[SteamStats] " + m_GuardiansDeployed + " Guardians Deployed");
            Debug.Log("[SteamStats] " + m_AssassinsDeployed + " Assassins Deployed");
        }
    }
}
