using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class AccGameManager : MonoBehaviourPun
{
    public GameObject playerPrefab;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("PrelimMenu");
            return;
        }
        else
        {
            StartCoroutine("PlaceInNetwork");
        }
    }

    private void Update()
    {
        Debug.Log(" Number Of Players : " + PhotonNetwork.CountOfPlayers);
    }

    //Instanciates A clone on network which reads values from VR player
    IEnumerator PlaceInNetwork()
    {
        yield return new WaitForSeconds(5f);
        PhotonNetwork.Instantiate("NetworkPlayer", Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate("TestObject", Vector3.zero, Quaternion.identity);

        Debug.Log("Objects Instanciated on Network From Acc Manager!");
    }
}
