using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;


[System.Serializable]
public class LeaderEntry 
{
   public CSteamID id;
   public int globalRank;
   public int score;
   public Sprite avatar;
   public Sprite trophyIcon;
    
    public LeaderEntry(CSteamID _id, int _globalRank, int _score)
    {
        id = _id;
        globalRank = _globalRank;
        score = _score;
        avatar = Sprite.Create(GetSmallAvatar(), new Rect(new Vector2(0, 0), new Vector2(32, 32)), new Vector2(32, 32));

        if (globalRank == 1)
        {
            trophyIcon = Resources.Load<Sprite>("Rank 1 Icon");
        }else if (globalRank == 2)
        {
            trophyIcon = Resources.Load<Sprite>("Rank 2 Icon");
        }
        else if (globalRank == 3)
        {
            trophyIcon = Resources.Load<Sprite>("Rank 3 Icon");
        }
        else
        {
            trophyIcon = null;
        }
 
    }
    

    private Texture2D GetSmallAvatar()
    {
      
        int FriendAvatar = SteamFriends.GetSmallFriendAvatar(id);
        uint ImageWidth;
        uint ImageHeight;
        bool success = SteamUtils.GetImageSize(FriendAvatar, out ImageWidth, out ImageHeight);

        if (success && ImageWidth > 0 && ImageHeight > 0)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];
            Texture2D returnTexture = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
            success = SteamUtils.GetImageRGBA(FriendAvatar, Image, (int)(ImageWidth * ImageHeight * 4));
            if (success)
            {
                returnTexture.LoadRawTextureData(Image);
                returnTexture.Apply();
            }
            return returnTexture;
        }
        else
        {
            Debug.LogError("Couldn't get avatar.");
            return new Texture2D(0, 0);
        }
    }
}
