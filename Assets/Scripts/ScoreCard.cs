using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ScoreCard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject itemPrefab;

    Dictionary<Player, ScoreBItems> scoreBitems = new Dictionary<Player, ScoreBItems>();
    private void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }

    //On New Player Enterd
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);
    }

    //On Player Left Room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreboardItem(Player player)
    {
        ScoreBItems item = Instantiate(itemPrefab, container).GetComponent<ScoreBItems>();
        item.InitializePlayer(player);
        scoreBitems[player] = item;
    }

    void RemoveScoreboardItem(Player player)
    {
        Destroy(scoreBitems[player].gameObject);
        scoreBitems.Remove(player);
    }
     public void UpdateScore(Player player){
        if(scoreBitems.ContainsKey(player)){
            scoreBitems[player].ScoreTxt.text = player.GetScore().ToString();
        }
     }
}
