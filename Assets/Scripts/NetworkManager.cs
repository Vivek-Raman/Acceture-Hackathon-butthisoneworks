using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//Handles Network Management
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Button btnConnectMaster;
    public Button btnConnectRoom;

    public bool triesToConnectToMaster;
    public bool triesToConnectToRoom;

    private void Start()
    {
        DontDestroyOnLoad(this);
        triesToConnectToMaster = false;
        triesToConnectToRoom = false;
    }

    void Update()
    {
        if (btnConnectMaster != null)
            btnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !triesToConnectToMaster);

        if (btnConnectRoom != null)
            btnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom);
    }

    public void ConnectToMaster()
    {

        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.NickName = "LPlayer";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "v1";

        triesToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings();

    }

    public void ConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        triesToConnectToRoom = true;
        //PhotonNetwork.CreateRoom("MyRoom");
        //PhotonNetwork.JoinRoom("MyRoom");
        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        triesToConnectToMaster = false;
        triesToConnectToRoom = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        triesToConnectToMaster = false;
        Debug.Log("Connected To Master!");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        triesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " Room is: " + PhotonNetwork.PlayerList);
        Destroy(FindObjectOfType<Valve.VR.InteractionSystem.Player>().gameObject);
        SceneManager.LoadScene("SampleScene") ;

        //StartCoroutine("PlaceInNetwork");

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        triesToConnectToRoom = false;
    }

    IEnumerator PlaceInNetwork()
    {
        yield return new WaitForSeconds(5f);
        GameObject go = PhotonNetwork.Instantiate("TestObject", Vector3.zero, Quaternion.identity);
        go.SetActive(true);

        Debug.Log("Objects Instanciated on Network From Net Manager!");
    }

}
