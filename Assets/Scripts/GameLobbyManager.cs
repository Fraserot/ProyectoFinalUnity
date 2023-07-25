using GameFramework.Core;
using GameFramework.Core.GameFramework.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameLobbyManager : Singleton<GameLobbyManager>
{

    public string GetLobbyCode()
    {
        return LobbyManager.Instance.GetLobbyCode();
    }

    public async Task<bool> CreateLobby()
    {
        Dictionary<string, string> playerData = new Dictionary<string, string>()
        {
            {"GamerTag", "HostPlayer" }
        };

        

        bool succeded = await LobbyManager.Instance.CreateLobby(30, true, playerData);

        return succeded;
    }
    
    public async Task <bool> JoinLobby(string code)
    {
        Dictionary<string, string> playerData = new Dictionary<string, string>()
        {
            {"GamerTag", "JoinPlayer" }
        };
        bool succeded = await LobbyManager.Instance.JoinLobby(code, playerData);
        return succeded;
    }
}
