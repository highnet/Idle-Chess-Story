using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfiler : MonoBehaviour
{
    public PlayerController playerController;
   

    // Start is called before the first frame update
    void Start()
    {
 
        playerController = GetComponent<PlayerController>();
    }
    public void SaveProfile(PlayerProfileSave data,int characterSlot)
    {
        Debug.Log("saving");
        PlayerPrefs.SetString("characterName_slot" + characterSlot, data.characterName);
        PlayerPrefs.SetString("rank_slot" + +characterSlot, data.rank);
        PlayerPrefs.SetString("UserIconImageName_slot" + +characterSlot, data.UserIconImageName);
        PlayerPrefs.SetInt("mmr_slot" + characterSlot, data.mmr);
        PlayerPrefs.SetInt("achievedRound_slot" + characterSlot, data.achievedRound);

        if (data.mmr > PlayerPrefs.GetInt("highestAchievedMmr_slot" + characterSlot ))
        {
            PlayerPrefs.SetInt("highestAchievedMmr_slot" + characterSlot, data.mmr);
        }

        if (data.achievedRound > PlayerPrefs.GetInt("highestAchievedRound_slot" + characterSlot))
        {
            PlayerPrefs.SetInt("highestAchievedRound_slot" + characterSlot, data.achievedRound);
        }

        PlayerPrefs.Save();
    }

    public PlayerProfileSave LoadProfile(int characterslot)
    {
            Debug.Log("loading");
            PlayerProfileSave loadedProfile = new PlayerProfileSave();
            loadedProfile.characterName = PlayerPrefs.GetString("characterName_slot" + characterslot);
            loadedProfile.rank = PlayerPrefs.GetString("rank_slot" + characterslot);
            loadedProfile.UserIconImageName = PlayerPrefs.GetString("UserIconImageName_slot" + characterslot);
            loadedProfile.mmr = PlayerPrefs.GetInt("mmr_slot" + characterslot);
            loadedProfile.highestAchievedMmr = PlayerPrefs.GetInt("highestAchievedMmr_slot" + characterslot);
            loadedProfile.highestAchievedRound = PlayerPrefs.GetInt("highestAchievedRound_slot" + characterslot);
            loadedProfile.achievedRound = PlayerPrefs.GetInt("achievedRound_slot" + characterslot);
        return loadedProfile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
