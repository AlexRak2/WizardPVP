using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;
using System;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby instance;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyDataUpdate_t> lobbyUpdate;

    protected Callback<LobbyMatchList_t> _lobbyMatchList;
    public CSteamID[] allLobbies;



    public Lobby CurrentLobby;

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
        lobbyUpdate = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            ReloadLobbyList();
            timer = 0;
        }
    }

    public void CreateLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, MyNetworkManager.singleton.maxConnections);
    }

    public static void JoinLobby(CSteamID id) 
    {
        SteamMatchmaking.JoinLobby(id);
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
        
        string lb = MainMenu.instance.lobbyName.text.Length > 0 ? MainMenu.instance.lobbyName.text : SteamFriends.GetPersonaName()+"'s lobby";
        CurrentLobby = new Lobby((CSteamID)callback.m_ulSteamIDLobby, lb);

        MyNetworkManager.singleton.StartHost();

        Debug.Log("Lobby created");
    }

    public void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    public void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) return;
        CurrentLobby = new Lobby((CSteamID)callback.m_ulSteamIDLobby);

        MyNetworkManager.singleton.networkAddress = CurrentLobby.owner.ToString();
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
        SteamMatchmaking.AddRequestLobbyListStringFilter(LobbyData.LobbyAssurance.ToString(), Lobby.lobbyAssurance, ELobbyComparison.k_ELobbyComparisonEqual);
        SteamMatchmaking.RequestLobbyList();
        isReloading = false;
    }

    void OnLobbyMatchList(LobbyMatchList_t param)
    {
        for (int i = 0; i < param.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyId = SteamMatchmaking.GetLobbyByIndex(i);
            
            SteamMatchmaking.RequestLobbyData(lobbyId);
        }
        LobbySpawner.instance.Refresh();
    }
    private void OnGetLobbyInfo(LobbyDataUpdate_t param)
    {
        if (param.m_ulSteamIDLobby == param.m_ulSteamIDMember) // Lobby data update
        {
            LobbySpawner.instance.SpawnLobby(new Lobby((CSteamID)param.m_ulSteamIDLobby));
        }
    }
    #endregion
}

public enum LobbyData
{
    Name,
    Status,
    Map,
    LobbyAssurance,
    MatchEnd,
    MatchLength,
    RespawnTime,
}

public struct Lobby
{

    public const string lobbyAssurance = "lol xD mais pour de vraih";
    public string Name => GetLobbyData<string>(LobbyData.Name);
    public CSteamID steamId;
    public int MemberCount => SteamMatchmaking.GetNumLobbyMembers(steamId);
    public int MaxMemberCount => SteamMatchmaking.GetLobbyMemberLimit(steamId);
    public CSteamID owner => SteamMatchmaking.GetLobbyOwner(steamId);

    public Lobby(CSteamID cSteamID)
    {
        steamId = cSteamID;
    }
    public Lobby(CSteamID cSteamID, string name)
    {
        steamId = cSteamID;
        if (SteamUser.GetSteamID() == owner)
        {
            SetLobbyData(LobbyData.LobbyAssurance, lobbyAssurance);
            SetLobbyData(LobbyData.Name, name);
        }
    }

    public T GetLobbyData<T>(LobbyData key)
    {
        string value = SteamMatchmaking.GetLobbyData(steamId, key.ToString());

        if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
        {
            value = value.Replace(',', '.'); // for some dumbfuck reason (different countries or whatever) sometimes unity uses a , as a decimal place instead of .
        }

        try
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
        catch (Exception ex)
        {
            // this is fine, whatever
            Debug.LogError($"Could not parse [{key}, {value}] as {typeof(T).Name}: {ex.Message}");
        }

        return default(T);
    }

    public void SetLobbyData(LobbyData key, object value)
    {
        Debug.Log("setting : " + key.ToString() + " to " + value.ToString());
        if (!SteamMatchmaking.SetLobbyData(steamId, key.ToString(), value.ToString()))
        {
            Debug.LogError("Error setting lobby data.");
        }
    }
}