using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Players : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float _fireRate = 0.3f;
    private const float MaxLife = 10;
    private float _lives = MaxLife;
    public float canFire = -1f;
    [SerializeField]
    private GameObject _laserPrefab;
    UIManager ui;
    [SerializeField] Transform Lasercontainer;
    // [SerializeField]
    // private GameObject _tripleShot;
    // private bool isTactive = false;
    PhotonView pv;
    public float _speed = 3f;
    private SpawnManager _spawner;
    private ScoreCard _scoreObj;
    [SerializeField]
    Image healthBarImg;
    [SerializeField]
    GameObject healthUI;
    [SerializeField] Text livesText;
    Player player;

    private int _score = 0;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _scoreObj = GameObject.Find("Scoreboard").GetComponent<ScoreCard>();
    }
    private void Start()
    {
        livesText.text = _lives.ToString();
        _spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            boundingObject();
            Laserfiring();
        }
        if(pv.Owner.GetScore() > _score){
            _score = pv.Owner.GetScore();
            _scoreObj.UpdateScore(pv.Owner);
        }

    }
    void boundingObject()
    {
        // variable for keys input
        float horizantalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizantalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);


        if (transform.position.x > 9.9f)
        {
            transform.position = new Vector3(-9.9f, transform.position.y, 0);
        }
        if (transform.position.x < -9.9f)
        {
            transform.position = new Vector3(9.9f, transform.position.y, 0);
        }
        if (transform.position.y > 6.6f)
        {
            transform.position = new Vector3(transform.position.x, -6.6f, 0);
        }
        if (transform.position.y < -6.6f)
        {
            transform.position = new Vector3(transform.position.x, 6.6f, 0);
        }
    }

    void Laserfiring()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            canFire = Time.time + _fireRate;
            // PhotonNetwork.Instantiate(_laserPrefab.name, transform.position + new Vector3(0, 1.6f, 0), Quaternion.identity);
            Laser laser = PhotonNetwork.Instantiate(_laserPrefab.name, Lasercontainer.position, Quaternion.identity).GetComponent<Laser>();
            laser.playerObject = this.gameObject.GetComponent<Players>();
            // if (isTactive == true)
            // {
            //     Instantiate(_tripleShot, transform.position + new Vector3(-0.04f, 4.08f, 0), Quaternion.identity);
            // }
        }
    }

    // Score 
    // public void AddScores()
    // {
        // pv.RPC("RPC_ScoreCalc", RpcTarget.All);
        // _scoreObj.AddScoreToBoard(pv.Owner);
        // pv.Owner.AddScore(10);
        // Debug.Log(pv.Owner.GetScore());
    // }
    // [PunRPC]
    // void RPC_ScoreCalc()
    // {
    //     if (!pv.IsMine)
    //     {
    //         return;
    //     }
    //     _scoreObj.AddScoreToBoard(pv.Owner);

    // }
    // public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps){
    //     Debug.Log(changedProps);
    //     if(!pv.IsMine && targetPlayer == pv.Owner){
    //         _scoreObj.AddScoreToBoard(targetPlayer);
    //     }
    // }

    // Handle Damage Player
    public void damagePlayer()
    {
        pv.RPC("RPC_Takedamage", RpcTarget.All);
    }

    [PunRPC]
    void RPC_Takedamage()
    {
        if (!pv.IsMine)
        {
            return;
        }
        _lives -= 1;
        healthBarImg.fillAmount = _lives / MaxLife;
        livesText.text = _lives.ToString();
        ui.showtext(_lives);
        if (_lives <= 0)
        {
            PhotonNetwork.Disconnect();
        }
    }

}
