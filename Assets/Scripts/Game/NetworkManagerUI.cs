using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class NetworkManagerUI : MonoBehaviour
    {
        [SerializeField] private Button buttonServer;
        [SerializeField] private Button buttonHost;
        [SerializeField] private Button buttonClient;

        private void Awake()
        {
            buttonServer.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartServer();
            });
            buttonHost.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartHost();
            });
            buttonClient.onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });
        }
    }
}
