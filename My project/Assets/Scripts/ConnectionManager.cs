using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button[] ButtonsMainMenu;

    [SerializeField] private TMP_InputField LobbyWanted;
    [SerializeField] private TextMeshProUGUI debugPhoton;

    private const string LobbyDefultName = "Our First Lobby";
    private const string LobbySecondName = "Our Second Lobby";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        for (int i = 0; i < ButtonsMainMenu.Length; i++) 
        {
            ButtonsMainMenu[i].enabled = false;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We succefully connected to photon");
        base.OnConnectedToMaster();

        if (ButtonsMainMenu.Length > 0)
        {
            ButtonsMainMenu[0].enabled = true;
        }
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //To join the lobby
    public void JoinLobby()
    {
        Debug.Log($"LobbyDefultName: {LobbyDefultName}, LobbySecondName: {LobbySecondName}, LobbyWanted.text: {LobbyWanted.text}");

        if (LobbySecondName == LobbyWanted.text)
        {
            Debug.Log("Joining LobbySecondName...");
            PhotonNetwork.JoinLobby(new TypedLobby(LobbySecondName, LobbyType.Default));
        }

        else
        {
            Debug.Log("Joining LobbyDefultName...");
            PhotonNetwork.JoinLobby(new TypedLobby(LobbyDefultName, LobbyType.Default));
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log($"We successfully joined the lobby {PhotonNetwork.CurrentLobby.Name}!");
    }

    //To join a room
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("MyRoom");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room created successfully!");
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We successfully joined the room " + PhotonNetwork.CurrentRoom);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
        }
    }

    public void Update()
    {
        debugPhoton.text = PhotonNetwork.NetworkClientState.ToString();
    }
}
