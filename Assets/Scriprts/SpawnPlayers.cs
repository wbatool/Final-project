using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;


    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    GameObject p1;
    GameObject p2;
    private void Start()
    {



        if (PhotonNetwork.IsMasterClient)
        {

            Vector2 randomPosition = new Vector2(Random.Range(-6, -5), Random.Range(2, 3));
            p1 = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            p1.tag = "player1";
            p1.transform.parent = transform;

        }

        else
        {
            Vector2 randomPosition = new Vector2(Random.Range(6, 5), Random.Range(2, 3));
            p2 = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            p2.tag = "player2";
            p2.transform.parent = transform;

        }
        if (p1 != null)
        {
            p1.tag = "player1";
            p1.transform.parent = transform;

        }
        else if (p2 != null)
        {
            p2.tag = "player2";

            p2.transform.parent = transform;
        }


    }

    public GameObject player;
    bool lock1;
    private void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null && !lock1)
        {
            lock1 = true;
            if (PhotonNetwork.IsMasterClient)
            {
                player.transform.parent = p1.transform.parent;
            }
            else
            {
                player.transform.parent = p2.transform.parent;
            }
        }
    }
}