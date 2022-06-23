using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ScoreBItems : MonoBehaviour
{
    public Text usernameTxt;
    public Text ScoreTxt;
    Dictionary <Player, string> scoreItem = new Dictionary<Player, string>();
    private void Start() {

    }
    public void InitializePlayer(Player player){
        player.SetScore(0);
        usernameTxt.text = player.NickName;
        ScoreTxt.text = player.GetScore().ToString();
        scoreItem.Add(player, ScoreTxt.text);
    }

}
