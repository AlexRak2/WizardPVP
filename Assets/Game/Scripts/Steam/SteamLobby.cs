using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;
using System;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby instance;
    private const string HostAddressKey = "HostAddress";

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;

    protected Callback<LobbyMatchList_t> _lobbyMatchList;
    public CSteamID[] allLobbies;



    public ulong LobbyID;

    public bool UsingSteam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        _lobbyMatchList = Callback<LobbyMatchList_t>.Create(OnLobbyMatchList);

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5)
        {
            ReloadLobbyList();
            timer = 0;
        }
    }

    public void CreateLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, MyNetworkManager.singleton.maxConnections);
    }

    public void JoinLobby() 
    {
        SteamMatchmaking.JoinLobby(allLobbies[0]);
    }

    #region Lobby Callbacks
    //CALLBACKS
    public void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) 
        {
            Debug.LogError("callback result failed on lobby creation");
            return;
        }

        MyNetworkManager.singleton.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", "untitledwizardgame");
        Debug.Log("Lobby created");
    }

    public void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    public void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return;

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        MyNetworkManager.singleton.networkAddress = hostAddress;
        MyNetworkManager.singleton.StartClient();
    }
    #endregion


    #region Lobby List Reload
    float timer;
    float nextTime;
    bool isReloading;

    public void ReloadLobbyList()
    {
        SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterWorldwide);
        SteamMatchmaking.RequestLobbyList();
        isReloading = false;
    }

    void OnLobbyMatchList(LobbyMatchList_t param)
    {
        //Getting an array of all lobbies
        allLobbies = new CSteamID[param.m_nLobbiesMatching];
        for (int i = 0; i < param.m_nLobbiesMatching; i++)
        {
            if (SteamMatchmaking.GetLobbyData(SteamMatchmaking.GetLobbyByIndex(i), "name") == "untitledwizardgame")
             allLobbies[i] = SteamMatchmaking.GetLobbyByIndex(i);
        }
    }
    #endregion
}