using UnityEngine;
using Mirror;
//https://vis2k.github.io/Mirror/Concepts/GameObjects/SpawnPlayerCustom

public class MyNetworkManager : NetworkManager {
    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage) {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
//brb :)