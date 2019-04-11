using System;
using Steamworks;

[Serializable]
public class PlayerSave
{
    public string Name;
    public int Mmr;

    public PlayerSave(PlayerController player)
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Name = name;
        }
        Mmr = player.playerMMR;
    }
}
