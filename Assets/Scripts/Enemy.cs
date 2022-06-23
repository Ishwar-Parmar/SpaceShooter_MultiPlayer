using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    private Players player;

    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    //Collide
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.transform.GetComponent<Players>();
            if (player != null)
            {
                player.damagePlayer();
            }
            Destroy(this.gameObject);
        }
    }
}
