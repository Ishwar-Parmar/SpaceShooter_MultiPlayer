using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{ 
    [SerializeField]
    private GameObject _enemyPrefab;
    // [SerializeField]
    // private GameObject[] powerUp;
    [SerializeField]
    private bool stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        // StartCoroutine(SpawnPowerUpRoutine());
    }
    //Coroutine
    IEnumerator SpawnEnemyRoutine(){
        while(stopSpawning == false && PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2){
            Vector3 spawnPos = new Vector3(Random.Range(-8.4f, 8.4f), 5.8f, 0);
            PhotonNetwork.Instantiate(_enemyPrefab.name, spawnPos,  Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        // if(PhotonNetwork.CurrentRoom.PlayerCount == 1){

        // }

    }
    // IEnumerator SpawnPowerUpRoutine(){
    //     while(stopSpawning == false){
    //         Vector3 spawnPos = new Vector3(Random.Range(-8.4f, 8.4f), 5.8f, 0);
    //         int powerBoost = Random.Range(0, 3);
    //         GameObject newShooter = Instantiate(powerUp[powerBoost], spawnPos, Quaternion.identity);
    //         yield return new WaitForSeconds(Random.Range(6f, 10f));
    //     }
    // }
    private void Update() {
        
    }
     public void onPlayerDeath(){
         stopSpawning = true;
     }
}
