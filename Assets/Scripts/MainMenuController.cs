using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button joinBtn;
    void Start()
    {
        hostBtn.onClick.AddListener(OnHostClick);
        joinBtn.onClick.AddListener(OnJoinClick);
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

    }
}
