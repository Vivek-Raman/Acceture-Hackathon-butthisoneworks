using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkVRPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject avatarHead;
    public GameObject body;
    public GameObject lhand;
    public GameObject rhand;
    public Transform globalPlayer;
    public Transform localPlayer;
    public Transform lhandPlayer;
    public Transform rhandPlayer;


    void Awake()
    {
        //is this is instanciated by the instance running on local system
        if (photonView.IsMine)
        {
            globalPlayer = GameObject.Find("Player").transform;
            localPlayer = globalPlayer.Find("SteamVRObjects/VRCamera").transform;
            lhandPlayer = globalPlayer.Find("SteamVRObjects/LeftHand/ObjectAttachmentPoint").transform;
            rhandPlayer = globalPlayer.Find("SteamVRObjects/RightHand/ObjectAttachmentPoint").transform;


            avatarHead.transform.SetParent(localPlayer);
            avatarHead.transform.localPosition = Vector3.zero;

            lhand.transform.SetParent(lhandPlayer);
            lhand.transform.localPosition = Vector3.zero;

            rhand.transform.SetParent(rhandPlayer);
            rhand.transform.localPosition = Vector3.zero;


            Debug.Log("Players Connected To Master  :" +  PhotonNetwork.CountOfPlayersOnMaster);
            Debug.Log("Players Connected To Some Room  :" + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("Players Connected To Some Room  :" + PhotonNetwork.CountOfRooms);


        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Player has Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnConnected();
        Debug.Log("Player has Joined Room");
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(globalPlayer.position);
            stream.SendNext(globalPlayer.rotation);
            stream.SendNext(localPlayer.localPosition);
            stream.SendNext(localPlayer.localRotation);
            stream.SendNext(lhandPlayer.position);
            stream.SendNext(lhandPlayer.rotation);
            stream.SendNext(rhandPlayer.localPosition);
            stream.SendNext(rhandPlayer.localRotation);
            Debug.Log("Reading");

        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatarHead.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatarHead.transform.localRotation = (Quaternion)stream.ReceiveNext();
            lhand.transform.localPosition = (Vector3)stream.ReceiveNext();
            lhand.transform.localRotation = (Quaternion)stream.ReceiveNext();
            rhand.transform.localPosition = (Vector3)stream.ReceiveNext();
            rhand.transform.localRotation = (Quaternion)stream.ReceiveNext();
            Debug.Log("Writing");
        }
    }
}
