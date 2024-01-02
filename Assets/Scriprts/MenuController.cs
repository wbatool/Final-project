using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuController : MonoBehaviourPunCallbacks
{ 
    [SerializeField] private string VersioName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private TMP_InputField UsernameInput; // Use TMP_InputField instead of InputField
    [SerializeField] private TMP_InputField CreateGameInput; // Use TMP_InputField instead of InputField
    [SerializeField] private TMP_InputField JoinGameInput; // Use TMP_InputField instead of InputField
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject MainMenu;
    private void Awake()
{
    PhotonNetwork.ConnectUsingSettings();
    PhotonNetwork.GameVersion = VersioName;
}

    private void Start()
    {
        UsernameMenu.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeusernameInput()
    {
        if(UsernameInput.text.Length>=3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUsername()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.NickName=UsernameInput.text;
    }

    public void CreateGame()
{
    // Check if already connected to the master server or lobby
    if (PhotonNetwork.InLobby || PhotonNetwork.InRoom)
    {
        // If already in the lobby or room, create a room directly
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 5 }, null);
    }
    else
    {
        // If not in the lobby or room, wait for the connection callback
        Debug.LogWarning("Wait for callback: OnJoinedLobby or OnConnectedToMaster");
    }
}


    public void JoinGame()
{
    // Check if connected to the master server or in the lobby
    if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }
    else
    {
        // Wait for callback: OnJoinedLobby or OnConnectedToMaster
        Debug.LogWarning("Wait for callback: OnJoinedLobby or OnConnectedToMaster");
    }
}


    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom called");
        MainMenu.SetActive(true); // Activate the loading panel
        ConnectPanel.SetActive(false); 
    }
}
