using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button[] ButtonsMainMenu;
    [SerializeField] private TextMeshProUGUI debugPhoton;

    private const string LobbyDefultName = "OurFirstLobby";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        debugPhoton.text = "We succefully connected to photon";
        base.OnConnectedToMaster();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("MyRoom");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        debugPhoton.text = "Room created successfully!";
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby(new TypedLobby(LobbyDefultName, LobbyType.Default));
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        debugPhoton.text = $"We successfully joined the lobby {PhotonNetwork.CurrentLobby}!";
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        debugPhoton.text = "We successfully joined the room " + PhotonNetwork.CurrentRoom;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
        }
    }

    private void Update()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
    }
}
