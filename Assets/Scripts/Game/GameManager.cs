using GameFramework.Core.GameFramework.Manager;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Lista de puntos de aparición

    void Start()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
        if (RelayManager.Instance.IsHost)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) = RelayManager.Instance.GetHostConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(ip, (ushort)port, allocationId, key, connectionData, true);
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip, int port) = RelayManager.Instance.GetClientConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port, allocationId, key, connectionData, hostConnectionData, true);
            NetworkManager.Singleton.StartClient();
        }
    }

    private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Pending = false;

        // Configurar la posición de aparición del jugador
        if (spawnPoints.Length > 0)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            response.Position = spawnPoints[randomSpawnIndex].position;
        }
        else
        {
            Debug.LogWarning("No se han configurado puntos de aparición (spawnPoints) en el GameManager.");
        }
    }
}
