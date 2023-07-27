using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LobbyPlayer: MonoBehaviour
    {
        [SerializeField] private TextMeshPro _PlayerName;
        private LobbyPlayerData _data;

        public void SetData(LobbyPlayerData data) 
        { 
            _data = data;
            _PlayerName.text = _data.GamerTag;
            gameObject.SetActive(true);
        }
    }
}
