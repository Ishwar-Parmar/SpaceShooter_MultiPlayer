using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField userName;
    public Text buttText;

    public void OnClickConnect(){
        if(userName.text.Length >= 1){
            PhotonNetwork.NickName = userName.text;
            buttText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster() {
        SceneManager.LoadScene("Lobby");
    }
}
