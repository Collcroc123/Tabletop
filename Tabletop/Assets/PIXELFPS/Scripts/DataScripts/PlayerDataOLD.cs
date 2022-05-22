using UnityEngine;
//using Steamworks;
using Mirror;

//[CreateAssetMenu(menuName = "Datas/PlayerData")]
public class PlayerDataOLD : ScriptableObject
{
    public string localName = "Player";
    //public string steamName;
    //public Texture steamImage;

    //[SyncVar(hook = nameof(SteamIdUpdated))] private ulong steamId;

    public void SetName(string newName)
    {
        localName = newName;
        if (localName == null) name = "Player";
    }
    /*
    #region STEAM
    
    public void SetSteamId(ulong steamId)
    { // Saves this player's Steam ID
        this.steamId = steamId;
    }
    
    public void SteamIdUpdated(ulong oldSteamId, ulong newSteamId)
    { // Loads and Sets User's Steam Name and Avatar
        var cSteamId = new CSteamID(newSteamId);
        steamName = SteamFriends.GetFriendPersonaName(cSteamId);
        int imageId = SteamFriends.GetLargeFriendAvatar(cSteamId);
        if (imageId != -1) steamImage = GetSteamImageAsTexture(imageId);
    }

    public void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
    { // Loads in User's Steam Avatar if Delayed
        if (callback.m_steamID.m_SteamID == steamId) steamImage = GetSteamImageAsTexture(callback.m_iImage);
    }

    public Texture2D GetSteamImageAsTexture(int iImage)
    { // Converts Steam Avatar Into Usable Image for Unity
        Texture2D texture = null;
        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[(width * height) * 4];
            isValid = SteamUtils.GetImageRGBA(iImage, image, (int) (width * height * 4));
            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        return texture;
    }
    #endregion
    */
}