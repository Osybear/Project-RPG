using UnityEngine;
using Mirror;
//https://vis2k.github.io/Mirror/Concepts/GameObjects/SpawnPlayerCustom
//https://vis2k.github.io/Mirror/Events/Server

public class MyNetworkManager : NetworkManager {

    //since this is getting overriden, it will ignore the start position gameobject placed
    //make sure to implement NetworkManager.startPositions
    /* 
    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage) {
        Debug.Log("OnServerAddPlayer()");
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
    */
}