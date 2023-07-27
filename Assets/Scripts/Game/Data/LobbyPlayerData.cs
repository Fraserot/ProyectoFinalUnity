using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class LobbyPlayerData : MonoBehaviour
{

    private string _id;
    private string _gamerTag;
    private bool _isReady;


    public string Id => _id;
    public string GamerTag => _gamerTag;
    public bool IsReady
    {
        get=> _isReady;
        set=> _isReady = value;
    }


    public void Initialize(string id, string gamertag)
    {
        _id = id;
        _gamerTag = gamertag;
    }

    public void Initialize(Dictionary<string, PlayerDataObject> playerData)
    {
        UpdateState(playerData);
    }

    public void UpdateState(Dictionary<string, PlayerDataObject> playerData)
    {
        if (playerData.ContainsKey("Id"))
        {
            _id = playerData["Id"].Value;
        }
        if (playerData.ContainsKey("Gamertag"))
        {
            _gamerTag = playerData["Gamertag"].Value;
        }
        if (playerData.ContainsKey("IsReady"))
        {
            _isReady = playerData["IsReady"].Value == "True";
        }

    }

    public Dictionary<string, string> Serialize()
    {
        return new Dictionary<string, string>()
        {
            { "Id", _id },
            { "Gamertag", _gamerTag },
            { "IsReady", _isReady.ToString() }
        };
} }

