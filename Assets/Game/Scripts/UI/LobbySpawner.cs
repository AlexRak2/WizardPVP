using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySpawner : MonoBehaviour
{

    public LobbyElement prefab;
    public Transform origin;
    public static LobbySpawner instance;

    public List<GameObject> lobbyList;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnLobby(Lobby lobby)
    {
        LobbyElement missUnderstood = Instantiate(prefab, origin);

        missUnderstood.lobbyId = lobby.steamId;
        missUnderstood.lobbyName.text = lobby.Name;
        lobbyList.Add(missUnderstood.gameObject);
    }

    public void Refresh()
    {
        for (int i = 0; i < lobbyList.Count; i++)
        {
            Destroy(lobbyList[i]);
        }
        lobbyList.Clear();
    }
}
