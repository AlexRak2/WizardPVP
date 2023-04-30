using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class LobbyElement : MonoBehaviour
{
    public CSteamID lobbyId;
    public TMP_Text lobbyName;

    public void Join()
    {
        SteamLobby.JoinLobby(lobbyId);
    }
}
