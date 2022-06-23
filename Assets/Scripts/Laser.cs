using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

// speed variable

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;
    public Players playerObject { get; set; }
    PhotonView Pv;

    private void Awake()
    {
        Pv = GetComponent<PhotonView>();
    }
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // destroy the object
        if (transform.position.y > 5.9)
        {
            Destroy(this.gameObject);
        }
    }

    //Collision with Enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && Pv.IsMine)
        {
            Pv.Owner.AddScore(10);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
