using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game 
{ 
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;
        [SerializeField] private Button _readyBtn;

        private void OnEnable()
        {
            _readyBtn.onClick.AddListener(OnReadyPressed);
        }

        private void OnDisable()
        {
            _readyBtn.onClick.RemoveAllListeners();

        }
        void Start()
        {
            _lobbyCodeText.text = $"Codigo: {GameLobbyManager.Instance.GetLobbyCode()}";
        }

        private async void OnReadyPressed()
        {
            bool succed = await GameLobbyManager.Instance.SetPlayerReady();
            if (succed)
            {
                _readyBtn.gameObject.SetActive(false);
            }
        }
    }
}