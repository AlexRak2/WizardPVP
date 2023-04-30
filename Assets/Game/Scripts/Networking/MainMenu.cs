using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

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

    public void JoinServer() 
    {
        if (SteamLobby.instance.UsingSteam)
        {
            SteamLobby.instance.JoinLobby();
        }
        else
        {
            MyNetworkManager.singleton.StartClient();
        }
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
