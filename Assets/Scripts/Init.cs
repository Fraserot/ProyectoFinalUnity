using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();

        if(UnityServices.State == ServicesInitializationState.Initialized)
        {


            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            //Para cuando conectemos los pinches nombres e info a la base de datos externa
            if (AuthenticationService.Instance.IsSignedIn)
            {
                string username = PlayerPrefs.GetString(key:"Username");
                if (username == "")
                {
                    username = "Alumno";
                    PlayerPrefs.SetString("Username", username);
                }

                SceneManager.LoadScene("MainMenu");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
