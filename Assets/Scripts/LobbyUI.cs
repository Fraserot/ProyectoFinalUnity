using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lobbyCodeText;

    void Start()
    {
        _lobbyCodeText.text = $"Codigo: {GameLobbyManager.Instance.GetLobbyCode()}";
    }
}
