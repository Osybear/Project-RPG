using UnityEngine;
using Mirror;
//https://vis2k.github.io/Mirror/Concepts/GameObjects/SpawnPlayerCustom

public class MyNetworkManager : NetworkManager {

    [Header("Local Player")]
    public GameObject localPlayer;

    /*
        when a client connects to the server. be able to get the other player prefabs
        to then change some variables.
     */
    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage) {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
