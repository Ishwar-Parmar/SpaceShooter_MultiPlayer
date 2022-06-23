using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefab;
    public Transform[] spawnPoints;
    private GameObject playerToSpawn;

    private void Start() {
        int randomNo = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNo];
        playerToSpawn = playerPrefab[0];
        if(PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] != null){
            playerToSpawn = playerPrefab[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];            
        }        
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
    
}
