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

    private const string UsernameKey = "Username";
    private const string RoomNameKey = "RoomName";
    //[SerializeField] private GameObject MainMenu;
    private void Awake()
{
    //PhotonNetwork.ConnectUsingSettings();
    PhotonNetwork.GameVersion = VersioName;
}

    private void Start()
    {
        UsernameMenu.SetActive(true);

        // Load stored username
        string storedUsername = PlayerPrefs.GetString(UsernameKey, "");
        UsernameInput.text = storedUsername;
        ChangeusernameInput(); 
    }

    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinLobby(TypedLobby.Default);
    //   Debug.Log("Connected");
    // }

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

        PlayerPrefs.SetString(UsernameKey, UsernameInput.text);
        PlayerPrefs.Save();
    }

    public void CreateRoom()
    {
         if(!string.IsNullOrEmpty(CreateGameInput.text))
        {
            RoomOptions roomOptions=new RoomOptions();
            roomOptions.MaxPlayers=10;

            PhotonNetwork.CreateRoom(CreateGameInput.text,roomOptions);

            Debug.Log("Creating room");
            //LoadingScreen.SetActive(true);
            //loadingText.text="Creating room...";

            PlayerPrefs.SetString(RoomNameKey, CreateGameInput.text);
            PlayerPrefs.Save();
        }
    }


    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinGameInput.text);
        Debug.Log("Room joined");
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom called");
        PhotonNetwork.LoadLevel(3);
    }

    public override void OnCreatedRoom()
{
    Debug.Log("Room created successfully!");
}

public override void OnCreateRoomFailed(short returnCode, string message)
{
    Debug.Log("Room creation failed: " + message);
}

public override void OnJoinRoomFailed(short returnCode, string message)
{
    Debug.Log("Joining room failed: " + message);
}

}
