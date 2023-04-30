using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TMP_InputField lobbyName;
    public TMP_Text countText;
    public Slider numberCount;


    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnSlider(float value)
    {
        countText.text = value.ToString();
    }

    public void HostServer() 
    {
        if (SteamLobby.instance.UsingSteam)
        {
            SteamLobby.instance.CreateLobby();
        }
        else 
        {
            MyNetworkManager.singleton.StartHost();
        }
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
