using GameFramework.Core;
using GameFramework.Core.GameFramework.Manager;
using GameFramework.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Game
{
    public class GameLobbyManager : Singleton<GameLobbyManager>
    {
        private List<LobbyPlayerData> _lobbyPLayerData = new List<LobbyPlayerData>();

        private LobbyPlayerData localLobbyPlayerData;
        private void OnEnable()
        {
            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
        }



        private void OnDisable()
        {
            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
        }
        // Método para obtener el código del lobby desde el LobbyManager
        public string GetLobbyCode()
        {
            return LobbyManager.Instance.GetLobbyCode();
        }

        // Método para crear un lobby de juego de forma asíncrona
        public async Task<bool> CreateLobby()
        {
            // Se crea un objeto LobbyPlayerData que representa los datos del jugador anfitrión.
            LobbyPlayerData playerData = new LobbyPlayerData();
            playerData.Initialize(AuthenticationService.Instance.PlayerId, "HostPlayer");

            // Se llama al método CreateLobby del LobbyManager para crear el lobby con los siguientes parámetros:
            // - Tiempo límite del lobby: 30 segundos
            // - Se permite iniciar el juego automáticamente cuando todos los jugadores están listos (true)
            // - Los datos del jugador anfitrión se serializan y se envían al lobby para que los demás jugadores puedan acceder a ellos.
            bool succeeded = await LobbyManager.Instance.CreateLobby(30, true, playerData.Serialize());

            // Se devuelve el resultado de la creación del lobby.
            return succeeded;
        }

        // Método para unirse a un lobby existente de forma asíncrona
        public async Task<bool> JoinLobby(string code)
        {

            LobbyPlayerData playerData = new LobbyPlayerData();
            playerData.Initialize(AuthenticationService.Instance.PlayerId, "JoinPlayer");
            // Se llama al método JoinLobby del LobbyManager para unirse al lobby con el código proporcionado y los datos del jugador.
            bool succeeded = await LobbyManager.Instance.JoinLobby(code, playerData.Serialize());

            // Se devuelve el resultado de unirse al lobby.
            return succeeded;
        }

        private void OnLobbyUpdated(Lobby lobby)
        {
            List<Dictionary<string, PlayerDataObject>> playerData = LobbyManager.Instance.GetPlayersData();
            _lobbyPLayerData.Clear();

            foreach (Dictionary<string, PlayerDataObject> data in playerData)
            {
                LobbyPlayerData lobbyPlayerData = new LobbyPlayerData();
                lobbyPlayerData.Initialize(data);


                if (lobbyPlayerData.Id == AuthenticationService.Instance.PlayerId)
                {
                    localLobbyPlayerData = lobbyPlayerData;
                }

                _lobbyPLayerData.Add(lobbyPlayerData);
            }

            Events.LobbyEvents.OnLobbyUpdated?.Invoke();
        }

        public List<LobbyPlayerData> GetPlayers()
        {
            return _lobbyPLayerData;
        }
    }

}