using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Manager : NetworkBehaviour {
    
	// Use this for initialization
	void Start () {
	}

    public void EndGame()
    {
        Invoke("BackLobby",3f);
    }
   

    void BackLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
}
