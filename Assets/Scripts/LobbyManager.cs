using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbypanel;
    public GameObject roompanel;
    public Text roomName;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetUpdates = 1.5f;
    float nextUpdate;

    List<PlayerItem> playerItemList = new List<PlayerItem>();
    public PlayerItem playerPrefab;
    public Transform playerItemParent;

    //gameobject for startbutton
    public GameObject playButton;

    private void Start() {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate(){
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 5, BroadcastPropsChangeToAll = true});
        }
    }

    public override void OnJoinedRoom(){
        lobbypanel.SetActive(false);
        roompanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerlist();

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        if(Time.time >= nextUpdate){
            UpdateRoomList(roomList);
            nextUpdate = Time.time + timeBetUpdates;
        }
        
    }

    void UpdateRoomList(List<RoomInfo> List){
        foreach (RoomItem roomItem in roomItemList)
        {
            Destroy(roomItem.gameObject);
        }
        roomItemList.Clear();

        foreach(RoomInfo room in List){
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName){
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom(){
        roompanel.SetActive(false);
        lobbypanel.SetActive(true);
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
    }

    // for Player Item list
    void UpdatePlayerlist(){
        foreach ( PlayerItem item in playerItemList)
        {
            Destroy(item.gameObject);
        }
        playerItemList.Clear();

        if(PhotonNetwork.CurrentRoom == null){
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayer = Instantiate(playerPrefab, playerItemParent);
            newPlayer.SetPlayerName(player.Value);
            if(player.Value == PhotonNetwork.LocalPlayer){
                newPlayer.ApplyChanges();
            }
            playerItemList.Add(newPlayer);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        UpdatePlayerlist();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdatePlayerlist();
    }
    private void Update() {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2){
            playButton.SetActive(true);
        }
        else{
            playButton.SetActive(false);
        }
    }

    public void OnClickplayButton(){
        PhotonNetwork.LoadLevel("Game");
    }
}
