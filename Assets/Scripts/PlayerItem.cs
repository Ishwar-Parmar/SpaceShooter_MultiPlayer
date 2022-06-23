using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;
    Image bgImage;
    // public Color highLightClr;
    public GameObject LeftButt;
    public GameObject RightButt;

    ExitGames.Client.Photon.Hashtable playerProp = new ExitGames.Client.Photon.Hashtable();
    public Image playerAvatar;
    public Sprite[] avatars;
    Player player;

    private void Start()
    {
        // bgImage = GetComponent<Image>();
    }
    public void SetPlayerName(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyChanges()
    {
        // bgImage.color = highLightClr;
        LeftButt.SetActive(true);
        RightButt.SetActive(true);
    }

    public void OnClickLeft()
    {
        // Debug.Log(player.CustomProperties["playerAvatar"]);
        if ((int)playerProp["playerAvatar"] == 0)
        {
            playerProp["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProp["playerAvatar"] = (int)playerProp["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProp);
    }

    public void OnClickRight()
    {
        if ((int)playerProp["playerAvatar"] == avatars.Length - 1)
        {
            playerProp["playerAvatar"] = 0;
        }
        else
        {
            playerProp["playerAvatar"] = (int)playerProp["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProp);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable playerProp)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProp["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProp["playerAvatar"] = 0;
        }
    }
}
