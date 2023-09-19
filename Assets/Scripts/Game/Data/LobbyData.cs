using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
namespace GameFramework.Core.Data
{
    public class LobbyData
    {
        private int _mapIndex;

        public int MapIndex
        {
            get => _mapIndex;
            set => _mapIndex = value;
        }

        public void Initialize(int mapIndex)
        {
            _mapIndex = mapIndex;
        }

        public void Initialize(Dictionary<string, DataObject> lobbyData)
        {
            UpdateState(lobbyData);
        }

        private void UpdateState(Dictionary<string, DataObject> lobbyData)
        {
            if (lobbyData.ContainsKey("MapIndex"))
            {
                _mapIndex = Int32.Parse(lobbyData["MapIndex"].Value);
            }
        }

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>() 
            {
                {"MapIndex", _mapIndex.ToString()}
            };
        }
    }
}

