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

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        clientID = (int)OwnerClientId;

        if (IsServer)
        {
            // Buscar el jugador existente con la etiqueta "Player".
            GameObject existingPlayer = GameObject.FindWithTag("Player");
            // Si se encuentra un jugador existente, simplemente cambia su posición.
            ChangePlayerPosition(existingPlayer);
            
        }
    }


    public void ChangePlayerPosition(GameObject player)
    {
        if (player != null)
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            Vector3 randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position;
            player.transform.position = randomSpawnPoint;
        }
    }
}
