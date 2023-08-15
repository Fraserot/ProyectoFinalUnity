using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LobbyPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _PlayerName;
        [SerializeField] private SpriteRenderer _isReady;
        private LobbyPlayerData _data;

        private void Start()
        {
            // Puedes realizar cualquier inicialización aquí si es necesario
        }

        public void SetData(LobbyPlayerData data)
        {
            _data = data;
            _PlayerName.text = _data.GamerTag;

            if (_data.IsReady)
            {
                // Cambiar el sprite 2D a verde
                if (_isReady != null)
                {
                    _isReady.color = Color.green;
                }
            }
            //else
            //{
            //    // Cambiar el sprite 2D a rojo (si es necesario)
            //    if (_isReady != null)
            //    {
            //        _isReady.color = Color.red;
            //    }
            //}

            gameObject.SetActive(true);
        }
    }
}

