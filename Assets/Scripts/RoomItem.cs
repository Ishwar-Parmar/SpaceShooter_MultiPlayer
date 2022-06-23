using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{  
    LobbyManager manager;

    private void Start() {
        manager = FindObjectOfType<LobbyManager>();
    }
    public Text roomName;
    public void SetRoomName(string _roomName){
        roomName.text = _roomName;
    }

    public void OnClickItem(){
        manager.JoinRoom(roomName.text);
    }

    
}
