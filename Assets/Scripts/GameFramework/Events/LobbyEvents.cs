using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace GameFramework.Events
{
    public static class LobbyEvents
    {
        public delegate void LobbyUpdated(Lobby lobby);
        public static LobbyUpdated OnLobbyUpdated;
    }
}