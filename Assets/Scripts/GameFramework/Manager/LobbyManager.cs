using GameFramework.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFramework.Core.GameFramework.Manager
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        private Lobby _lobby; // Almacena la referencia al lobby creado.
        private Coroutine _heartBeatCoroutine;
        private Coroutine _refreshLobbyCoroutine;

        public string GetLobbyCode()
        {
            return _lobby?.LobbyCode;
        }

        // Método para crear un nuevo lobby.
        public async Task<bool> CreateLobby(int maxPlayers, bool isPrivate, Dictionary<string, string> data,Dictionary<string, string> lobbyData)
        {
            // Serializa los datos del jugador en el formato necesario para el lobby.
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);

            // Crea una nueva instancia del jugador utilizando el servicio de autenticación de Unity.
            Player player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);

            // Crea las opciones de creación del lobby.
            CreateLobbyOptions options = new CreateLobbyOptions()
            {
                Data = SerializeLobbyData(lobbyData),
                IsPrivate = isPrivate,
                Player = player 
            };
            try
            {
                // Llama al servicio de lobbies de Unity para crear un nuevo lobby de forma asíncrona.

                _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", maxPlayers, options);
            }
            catch (System.Exception)
            {
                return false;
            }

           
            Debug.Log("Lobby Creado con un ID: " + _lobby.Id);

            _heartBeatCoroutine= StartCoroutine(HeartbeatrLobbyCoroutine(_lobby.Id, 6f));
            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(_lobby.Id, 1f));

            return true; 
        }

        private Dictionary<string, DataObject> SerializeLobbyData(Dictionary<string, string> data)
        {
            Dictionary<string, DataObject> lobbyData = new Dictionary<string, DataObject>();

            foreach (var(key, value) in data)
            {
                lobbyData.Add(key, new DataObject (
                        visibility: DataObject.VisibilityOptions.Member,
                        value: value
                    ));
            }

            return lobbyData;
        }

        //Contar los pulsos del server
        private IEnumerator HeartbeatrLobbyCoroutine(string lobbyId, float waitTimeSeconds)
        {
            while (true)
            {
                Debug.Log("Heartbeat");
                LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
                yield return new WaitForSeconds(waitTimeSeconds);
            }
        }

        private IEnumerator RefreshLobbyCoroutine(string lobbyId, float waitTimeSeconds)
        {
            while (true)
            {
                Task<Lobby> task = LobbyService.Instance.GetLobbyAsync(lobbyId);
                yield return new WaitUntil(() => task.IsCompleted);
                Lobby newLobby = task.Result;
                if (newLobby.LastUpdated > _lobby.LastUpdated) 
                {
                    _lobby = newLobby;
                    LobbyEvents.OnLobbyUpdated.Invoke(_lobby);
                }
                yield return new WaitForSeconds(waitTimeSeconds);
            }
        }

        // Método para serializar los datos del jugador en el formato requerido por el lobby.
        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            Dictionary<string, PlayerDataObject> playerData = new Dictionary<string, PlayerDataObject>();
            foreach (var (key, value) in data)
            {
                // Crea un objeto PlayerDataObject para cada dato del jugador y lo agrega al diccionario playerData.
                playerData.Add(key, new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Member,
                    value: value
                ));
            }
            return playerData;
        }

        public void OnApplicationQuit()
        {
            if (_lobby != null && _lobby.Id == AuthenticationService.Instance.PlayerId)
            {
                // Si existe un lobby y su ID coincide con el ID del jugador autenticado, se elimina el lobby de forma asíncrona.
                LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
            }
        }

        public async Task<bool> JoinLobby(string code, Dictionary<string, string> playerData)
        {
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions();
            Player player = new Player(AuthenticationService.Instance.PlayerId, null, SerializePlayerData(playerData));
            
            options.Player = player;
            try
            {
                _lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code, options);
            }
            catch (SystemException)
            {
                return false;
            }

            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(_lobby.Id, 1f));
            return true;

        }

        public List<Dictionary<string, PlayerDataObject>> GetPlayersData()
        {
            List<Dictionary<string, PlayerDataObject>> data = new List<Dictionary<string, PlayerDataObject>> ();
            foreach (Player player in _lobby.Players)
            {
                data.Add(player.Data);
            }

            return data;
        }

        public async Task<bool> UpdatePlayerData(string playerID, Dictionary<string, string> data)
        {
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = playerData
            };
            try
            {
                _lobby = await LobbyService.Instance.UpdatePlayerAsync(_lobby.Id, playerID,options);

            }
            catch(System.Exception)
            {
                return false;
            }

            LobbyEvents.OnLobbyUpdated(_lobby);

            return true;
        }

        public async Task<bool> UpdateLobbyData(Dictionary<string,string> data)
        {
            Dictionary<string, DataObject> lobbyData = SerializeLobbyData(data);

            UpdateLobbyOptions options = new UpdateLobbyOptions()
            {
                Data = lobbyData

            };

            try
            {
              _lobby=  await LobbyService.Instance.UpdateLobbyAsync(_lobby.Id, options);
            }
            catch (System.Exception)
            {
                return false;
            }
            LobbyEvents.OnLobbyUpdated(_lobby);

            return true;
        }
    }
}
