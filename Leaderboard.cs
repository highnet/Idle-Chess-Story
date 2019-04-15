﻿using UnityEngine;
using Steamworks;
using System.Collections;
using System.Threading;

public class Leaderboard : MonoBehaviour
{
    public UiController uic;
    public PlayerController pc;
    private CallResult<LeaderboardFindResult_t> m_LeaderBoardResults;
    private CallResult<LeaderboardScoreUploaded_t> m_LeaderBoardScoreUploaded;
    private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardUserScoreDownloaded;
    private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardTop10Downloaded;
    public SteamLeaderboard_t hSteamLeaderboard;
    public SteamLeaderboardEntries_t hSteamLeaderboardEntry;
    public SteamLeaderboardEntries_t hSteamTop10Entries;
    public ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate;
    public int[] pScoreDetails = new int[0];
    public int cScoreDetailsCount = 0;
    public string SourceLeaderBoard = "TEST";
    public bool  foundLeaderboard = false;
    public bool downLoadingUserEntry = false;
    public bool downLoadingTop10Entries = false;
    public bool downloadedLeaderboard = false;
    public int topEntriesCount;
    public LeaderEntry[] top10Entries;

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_LeaderBoardResults = CallResult<LeaderboardFindResult_t>.Create(OnLeaderBoardResults);
            m_LeaderBoardScoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderBoardScoresUploaded);
            m_LeaderBoardUserScoreDownloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnleaderboardScoresDownloaded);
            m_LeaderBoardTop10Downloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardTop10Downloaded);
        }   
    }

    private void OnleaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_cEntryCount == 0)
        {
            Debug.Log("No entries Found");
            SetLeaderBoardScore(1200);
            uic.intro_playerMMR.text = "mmr: 1200";
            pc.playerMMR = 1200;
            pc.newPlayer = true;
            uic.fetchSteamLeaderboardEntry = false;
        }
        else
        {
            Debug.Log(pCallback.m_cEntryCount + " entry found");
            hSteamLeaderboardEntry = pCallback.m_hSteamLeaderboardEntries;
            downLoadingUserEntry = false;
            downloadedLeaderboard = true;

        }
    }

    private void OnLeaderboardTop10Downloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_cEntryCount == 0)
        {
            Debug.Log("Top10: No entries Found");
        }
        else
        {
            Debug.Log("Top10: " + pCallback.m_cEntryCount + " entries found");
            topEntriesCount = pCallback.m_cEntryCount;
            hSteamTop10Entries = pCallback.m_hSteamLeaderboardEntries;
            downLoadingTop10Entries = false;
        }
    }

    private void OnLeaderBoardResults(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bLeaderboardFound == 0 || bIOFailure)
        {
            Debug.Log("There was an error finding leaderboard.");

        }
        else
        {
            hSteamLeaderboard = pCallback.m_hSteamLeaderboard;
            Debug.Log("Leaderboard Found: " + hSteamLeaderboard);
            foundLeaderboard = true;

        }
    }

    private void OnLeaderBoardScoresUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess == 0 || bIOFailure)
        {
            Debug.Log("There was an error uploading scores.");
            Debug.Log(pCallback.m_bSuccess);
            Debug.Log(hSteamLeaderboard);
            Debug.Log(pCallback.m_hSteamLeaderboard);
        }
        else
        {
            Debug.Log("Score Uploaded to Leaderboard");
        }
    }

    void Start()
    {
        FindLeaderBoard();
    }

    private void Update()
    {
        if (foundLeaderboard && !downloadedLeaderboard && !downLoadingUserEntry)
        {
            DownloadUserEntry();
        }
        if (foundLeaderboard && !downloadedLeaderboard && !downLoadingTop10Entries)
        {
            DownloadTop10Entries();
        }
      
    }

    public void FindLeaderBoard()
    {
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard(SourceLeaderBoard);
        m_LeaderBoardResults.Set(handle);
    }

    public void SetLeaderBoardScore(int score)
    {
        Debug.Log("Setting leaderboard score to: " + score);
        if (score >= 0 && score <= 5000)
        {
            SteamAPICall_t handle2 = SteamUserStats.UploadLeaderboardScore(hSteamLeaderboard, eLeaderboardUploadScoreMethod, score, pScoreDetails, cScoreDetailsCount);
            m_LeaderBoardScoreUploaded.Set(handle2);
        }
     
    }

    public void DownloadUserEntry()
    {
        Debug.Log("downloading your leaderboard entry");
        downLoadingUserEntry = true;
        CSteamID[] cSteamID = new CSteamID[1];
        cSteamID[0] = SteamUser.GetSteamID();
        SteamAPICall_t handle3 = SteamUserStats.DownloadLeaderboardEntriesForUsers(hSteamLeaderboard,cSteamID,1);
        m_LeaderBoardUserScoreDownloaded.Set(handle3); // set m_LeaderBoardUserScoreDownloaded with the user downloaded leaderboard entry
    }

    public void DownloadTop10Entries()
    {
        Debug.Log("downloading top 10 leaderboard entries");
        downLoadingTop10Entries = true;
        SteamAPICall_t handle4 = SteamUserStats.DownloadLeaderboardEntries(hSteamLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal,0,11);
        m_LeaderBoardTop10Downloaded.Set(handle4); // set m_LeaderBoardTop10Downloaded with the top 10 downloaded leaderboard entries
    }
    
    public LeaderEntry[] ReadDownloadedTop10Entries()
    {
      
        Debug.Log("reading top 10 entries");
        LeaderEntry[] vals = new LeaderEntry[topEntriesCount];
        
        for (int i = 0; i < topEntriesCount; i++)
        {
            LeaderboardEntry_t entry_T;
            SteamUserStats.GetDownloadedLeaderboardEntry(hSteamTop10Entries, i, out entry_T, pScoreDetails, cScoreDetailsCount);
            vals[i] = new LeaderEntry(entry_T.m_steamIDUser, entry_T.m_nGlobalRank,entry_T.m_nScore);
            
        }
        return vals;
      
    }

    public int ReadDownloadedUserLeaderboardEntry()
    {
        Debug.Log("reading your leaderboard entry");
        LeaderboardEntry_t entry_T;
        SteamUserStats.GetDownloadedLeaderboardEntry(hSteamLeaderboardEntry, 0, out entry_T, pScoreDetails, cScoreDetailsCount);
        return entry_T.m_nScore;
    }

}