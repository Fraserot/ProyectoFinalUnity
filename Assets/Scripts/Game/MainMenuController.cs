using System.Collections;
using System.Collections.Generic;
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
            code = code.Substring(0, code.Length - 1);

            bool succeded = await GameLobbyManager.Instance.JoinLobby(code);
            if (succeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }

        }
    }
}