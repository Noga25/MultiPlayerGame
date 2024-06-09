using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.UI;
using Photon.Pun.UtilityScripts;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button[] ButtonsMainMenu;

    [SerializeField] private TMP_InputField LobbyWanted;
    [SerializeField] private TextMeshProUGUI debugPhoton;
    [SerializeField] private TextMeshProUGUI RoomInfo;

    private const string LobbyDefultName = "Our First Lobby";
    private const string LobbySecondName = "Our Second Lobby";

    private void InitialButtons()
    {
        for (int i = 0; i < ButtonsMainMenu.Length; i++)
        {
            ButtonsMainMenu[i].enabled = false;

            if (i != 0 && i < ButtonsMainMenu.Length)
            {
                ButtonsMainMenu[i].gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        InitialButtons();
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
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
        ActiveRooms();
    }

    public void ActiveRooms()
    {
        if (LobbySecondName == LobbyWanted.text)
        {
            for (int i = 1; i < ButtonsMainMenu.Length; i++)
            {
                ButtonsMainMenu[i].enabled = true;
                ButtonsMainMenu[0].enabled = false;

                if (i != 2 && i != 5 && i < ButtonsMainMenu.Length)
                {
                    ButtonsMainMenu[i].gameObject.SetActive(true);
                }
            }
        }

        else
        {
            for (int i = 1; i < ButtonsMainMenu.Length; i++)
            {
                ButtonsMainMenu[i].enabled = true;
                ButtonsMainMenu[0].enabled = false;

                if (i != 1 && i != 4 && i < ButtonsMainMenu.Length)
                {
                    ButtonsMainMenu[i].gameObject.SetActive(true);
                }
            }
        }
    }

    //To join a room
    public void CreateAndJoinRoom(int maxPlayers)
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = maxPlayers,
        };

        if (PhotonNetwork.CurrentLobby.Name == LobbyDefultName)
        {
            PhotonNetwork.CreateRoom("Room 1", roomOptions, null);
        }

        else if (PhotonNetwork.CurrentLobby.Name == LobbySecondName)
        {
            PhotonNetwork.CreateRoom("Room 2", roomOptions, null);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room created successfully!");

        Room room = PhotonNetwork.CurrentRoom;

        if (RoomInfo != null && PhotonNetwork.CurrentRoom != null)
        {
            RoomInfo.text = $"Room: {room.Name} - Players: {room.PlayerCount.ToString() + " / " + room.MaxPlayers.ToString()}";
        }
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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
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
