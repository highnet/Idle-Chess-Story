using UnityEngine;
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
    private CallResult<LeaderboardScoresDownloaded_t> m_LeaderBoardTop100Downloaded;
    private SteamLeaderboard_t hSteamLeaderboard;
    private SteamLeaderboardEntries_t hSteamLeaderboardEntry;
    private SteamLeaderboardEntries_t hSteamTop100Entries;
    private ELeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate;
    private int[] pScoreDetails = new int[0];
    private int cScoreDetailsCount = 0;
    private string SourceLeaderBoard = "TEST";
    public bool  foundLeaderboard = false;
    public bool downLoadingUserEntry = false;
    public bool downLoadingTop100Entries = false;
    public bool downloadedLeaderboard = false;
    public int topEntriesCount;
    public LeaderEntry[] top100Entries;

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_LeaderBoardResults = CallResult<LeaderboardFindResult_t>.Create(OnLeaderBoardResults);
            m_LeaderBoardScoreUploaded = CallResult<LeaderboardScoreUploaded_t>.Create(OnLeaderBoardScoresUploaded);
            m_LeaderBoardUserScoreDownloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnleaderboardScoresDownloaded);
            m_LeaderBoardTop100Downloaded = CallResult<LeaderboardScoresDownloaded_t>.Create(OnLeaderboardTop100Downloaded);
        }   
    }

    private void OnleaderboardScoresDownloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_cEntryCount == 0)
        {
            Debug.Log("[LeaderBoard] No entries Found");
            SetLeaderBoardScore(1200);
            uic.intro_playerMMR.text = "mmr: 1200";
            pc.playerMMR = 1200;
            pc.newPlayer = true;
            uic.fetchSteamLeaderboardEntry = false;
        }
        else
        {
            Debug.Log("[LeaderBoard] " + pCallback.m_cEntryCount + " entry found");
            hSteamLeaderboardEntry = pCallback.m_hSteamLeaderboardEntries;
            downLoadingUserEntry = false;
            downloadedLeaderboard = true;

        }
    }

    private void OnLeaderboardTop100Downloaded(LeaderboardScoresDownloaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_cEntryCount == 0)
        {
            Debug.Log("[LeaderBoard] Top100: No entries Found");
        }
        else
        {
            Debug.Log("[LeaderBoardTop100] Top100: " + pCallback.m_cEntryCount + " entries found");
            topEntriesCount = pCallback.m_cEntryCount;
            hSteamTop100Entries = pCallback.m_hSteamLeaderboardEntries;
            downLoadingTop100Entries = false;
        }
    }

    private void OnLeaderBoardResults(LeaderboardFindResult_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bLeaderboardFound == 0 || bIOFailure)
        {
            Debug.Log("[LeaderBoard] There was an error finding leaderboard.");

        }
        else
        {
            hSteamLeaderboard = pCallback.m_hSteamLeaderboard;
            Debug.Log("[LeaderBoard] Leaderboard Found: " + hSteamLeaderboard);
            foundLeaderboard = true;

        }
    }

    private void OnLeaderBoardScoresUploaded(LeaderboardScoreUploaded_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess == 0 || bIOFailure)
        {
            Debug.Log("[LeaderBoard] There was an error uploading scores.");
        }
        else
        {
            Debug.Log("[LeaderBoard] Score Uploaded to Leaderboard");
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
        if (foundLeaderboard && !downloadedLeaderboard && !downLoadingTop100Entries)
        {
            DownloadTop100Entries();
        }
      
    }

    public void FindLeaderBoard()
    {
        SteamAPICall_t handle = SteamUserStats.FindLeaderboard(SourceLeaderBoard);
        m_LeaderBoardResults.Set(handle);
    }

    public void SetLeaderBoardScore(int score)
    {
        Debug.Log("[LeaderBoard] Setting leaderboard score to: " + score);
        if (score >= 0 && score <= 5000)
        {
            SteamAPICall_t handle2 = SteamUserStats.UploadLeaderboardScore(hSteamLeaderboard, eLeaderboardUploadScoreMethod, score, pScoreDetails, cScoreDetailsCount);
            m_LeaderBoardScoreUploaded.Set(handle2);
        }
     
    }

    public void DownloadUserEntry()
    {
        Debug.Log("[LeaderBoard] downloading your leaderboard entry");
        downLoadingUserEntry = true;
        CSteamID[] cSteamID = new CSteamID[1];
        cSteamID[0] = SteamUser.GetSteamID();
        SteamAPICall_t handle3 = SteamUserStats.DownloadLeaderboardEntriesForUsers(hSteamLeaderboard,cSteamID,1);
        m_LeaderBoardUserScoreDownloaded.Set(handle3); // set m_LeaderBoardUserScoreDownloaded with the user downloaded leaderboard entry
    }

    public void DownloadTop100Entries()
    {
        Debug.Log("[LeaderBoard] downloading top 10 leaderboard entries");
        downLoadingTop100Entries = true;
        SteamAPICall_t handle4 = SteamUserStats.DownloadLeaderboardEntries(hSteamLeaderboard,ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal,0,11);
        m_LeaderBoardTop100Downloaded.Set(handle4); // set m_LeaderBoardTop10Downloaded with the top 10 downloaded leaderboard entries
    }
    
    public LeaderEntry[] ReadDownloadedTop100Entries()
    {
      
        Debug.Log("[LeaderBoard] reading top 10 entries");
        LeaderEntry[] vals = new LeaderEntry[topEntriesCount];
        
        for (int i = 0; i < topEntriesCount; i++)
        {
            LeaderboardEntry_t entry_T;
            SteamUserStats.GetDownloadedLeaderboardEntry(hSteamTop100Entries, i, out entry_T, pScoreDetails, cScoreDetailsCount);
            vals[i] = new LeaderEntry(entry_T.m_steamIDUser, entry_T.m_nGlobalRank,entry_T.m_nScore);
            
        }
        return vals;
      
    }

    public int ReadDownloadedUserLeaderboardEntry()
    {
        Debug.Log("[LeaderBoard] reading your leaderboard entry");
        LeaderboardEntry_t entry_T;
        SteamUserStats.GetDownloadedLeaderboardEntry(hSteamLeaderboardEntry, 0, out entry_T, pScoreDetails, cScoreDetailsCount);
        return entry_T.m_nScore;
    }

}
