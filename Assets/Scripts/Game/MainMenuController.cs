using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class MainMenuController : MonoBehaviour
    {

        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject JoinScreen;
        [SerializeField] private Button hostBtn;
        [SerializeField] private Button joinBtn;
        [SerializeField] private Button reJoinBtn;
        [SerializeField] private Button leaveBtn;
        [SerializeField] private Button sumbitCodeBtn;
        [SerializeField] private TextMeshProUGUI codeText;
        void OnEnable()
        {
            hostBtn.onClick.AddListener(OnHostClick);
            joinBtn.onClick.AddListener(OnJoinClick);
            sumbitCodeBtn.onClick.AddListener(OnSumbitCodeClick);

        }
        

        private void OnDisable()
        {
            hostBtn.onClick.RemoveListener(OnHostClick);
            joinBtn.onClick.RemoveListener(OnJoinClick);
            sumbitCodeBtn.onClick.RemoveListener(OnSumbitCodeClick);

        }
        private async void Start()
        {
            if (await GameLobbyManager.Instance.HasActiveLobbies())
            {
                hostBtn.gameObject.SetActive(false);
                joinBtn.gameObject.SetActive(false);


                reJoinBtn.gameObject.SetActive(true);
                leaveBtn.gameObject.SetActive(true);
                reJoinBtn.onClick.AddListener(OnReJoinClick);
                leaveBtn.onClick.AddListener(OnLeaveGameClick);

            }
        }

        //Algunos errores, pero si funciona
        private async void OnLeaveGameClick()
        {
            bool succeded = await GameLobbyManager.Instance.LeaveAllLobby();
            if (succeded)
            {
                Debug.Log("Todos los lobbies han sido desconectados");
                hostBtn.gameObject.SetActive(true);
                joinBtn.gameObject.SetActive(true);


                reJoinBtn.gameObject.SetActive(false);
                leaveBtn.gameObject.SetActive(false);

            }
        }

        private async void OnReJoinClick()
        {
            bool succeded = await GameLobbyManager.Instance.RejoinGame();
            if (succeded)
            {
                SceneManager.LoadSceneAsync("Lobby");

            }
        }
        private async void OnHostClick()
        {
            bool succeded = await GameLobbyManager.Instance.CreateLobby();
            if (succeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }
        }
        private void OnJoinClick()
        {
            mainScreen.SetActive(false);
            JoinScreen.SetActive(true);
        }

        private async void OnSumbitCodeClick()
        {

            string code = codeText.text;
            code = code[..^1];

            bool succeded = await GameLobbyManager.Instance.JoinLobby(code);
            if (succeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }

        }
    }
}