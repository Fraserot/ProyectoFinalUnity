using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameFramework.Core.Data;
using Game.Events;

namespace Game 
{ 
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;
        [SerializeField] private Button _readyBtn;
        [SerializeField] private Button _startBtn;
        [SerializeField] private Image _mapImage;
        [SerializeField] private Button _leftBtn;
        [SerializeField] private Button _rightBtn;
        [SerializeField] private TextMeshProUGUI _mapName;
        [SerializeField] private TextMeshProUGUI _mapName2;
        [SerializeField] private MapSelectionData _mapSelectionData;


        private int _currentMapIndex = 0;

        private void OnEnable()
        {
            _readyBtn.onClick.AddListener(OnReadyPressed);

            //Comprobacion para que solo el ID del host pueda cambiar el mapa
            if (GameLobbyManager.Instance.IsHost)
            {
                _rightBtn.onClick.AddListener(OnRightButtonClicked);
                _leftBtn.onClick.AddListener(OnLeftButtonClicked);
                _startBtn.onClick.AddListener(OnStartBtnClicked);

                LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
                LobbyEvents.OnLobbyReady += OnLobbyReady;
            }

        }



        private void OnDisable()
        {
            _readyBtn.onClick.RemoveAllListeners();
            _rightBtn.onClick.RemoveAllListeners();
            _leftBtn.onClick.RemoveAllListeners();
            _startBtn.onClick.RemoveAllListeners();


            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
            LobbyEvents.OnLobbyReady -= OnLobbyReady;



        }
        void Start()
        {
            _lobbyCodeText.text = $"Codigo: {GameLobbyManager.Instance.GetLobbyCode()}";
            if (!GameLobbyManager.Instance.IsHost)
            {
                _leftBtn.gameObject.SetActive(false);
                _rightBtn.gameObject.SetActive(false);
            }
        }

        private async void OnLeftButtonClicked()
        {
            if (_currentMapIndex - 1 > 0)
            {
                _currentMapIndex--;
            }
            else
            {
                _currentMapIndex=0;
            }

            UpdateMap();
            await GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex);
        }



        private async void OnRightButtonClicked()
        {
            {
                if (_currentMapIndex + 1 < _mapSelectionData.Maps.Count-1)
                {
                    _currentMapIndex++;
                }
                else
                {
                    _currentMapIndex = _mapSelectionData.Maps.Count - 1;
                }
                UpdateMap();
               await GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex);

            }
        }

        private async void OnReadyPressed()
        {
            bool succed = await GameLobbyManager.Instance.SetPlayerReady();
            if (succed)
            {
                _readyBtn.gameObject.SetActive(false);
            }
        }



        private void UpdateMap()
        {
            // Obtén la textura de MapThumbnail
            Texture2D mapTexture = _mapSelectionData.Maps[_currentMapIndex].MapThumbnail;

            // Crea un sprite a partir de la textura y asígnalo al Image
            Sprite sprite = Sprite.Create(mapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), new Vector2(0.5f, 0.5f));
            _mapImage.sprite = sprite;


            _mapName.text = _mapSelectionData.Maps[_currentMapIndex].MapName;
            _mapName2.text = _mapSelectionData.Maps[_currentMapIndex].MapName;
        }

        private void OnLobbyUpdated()
        {
            _currentMapIndex =  GameLobbyManager.Instance.GetMapIndex();
            UpdateMap();
        }
        private void OnLobbyReady()
        {
            _startBtn.gameObject.SetActive(true);
        }

        private async void OnStartBtnClicked()
        {
             await GameLobbyManager.Instance.StartGame(_mapSelectionData.Maps[_currentMapIndex].SceneName);
        }
    }
}