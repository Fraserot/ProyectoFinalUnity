using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Unity.Netcode;
using System.Security.Principal;

public class PlayerNetworking : NetworkBehaviour
{
    public GameObject playerPrefab;
    public int clientID;
    private GameObject mPlayer;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        clientID = (int)OwnerClientId;

        if (IsServer)
            mPlayer = SpawnPlayer();
    }


    public GameObject SpawnPlayer()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        GameObject go = Instantiate(playerPrefab,
            spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        go.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
        return go;
    }

}
