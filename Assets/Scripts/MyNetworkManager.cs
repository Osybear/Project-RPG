using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;  
using TMPro;

public class MyNetworkManager : NetworkManager {

    [Header("Login Info")]
    public DatabaseManager databaseManager;
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;    

    class CredentialsMessage : MessageBase {
        // use whatever credentials make sense for your game
        // for example,  you might want to pass the accessToken if using oauth
        public string username;
        public string password;
    }

    // this gets called in your client after 
    // it has connected to the server,  
    public override void OnClientConnect(NetworkConnection connection) {
        base.OnClientConnect(connection);

        var msg = new CredentialsMessage() {
            // perhaps get the username and password
            // from textboxes instead
            username = username.text,
            password = password.text
        };

        ClientScene.AddPlayer(connection, MessagePacker.Pack(msg));
    }

    // this gets called in your server when the 
    // client requests to add a player.
    public override void OnServerAddPlayer(NetworkConnection connection, AddPlayerMessage extraMessage) {
        var msg = MessagePacker.Unpack<CredentialsMessage>(extraMessage.value);

        // check the credentials by calling your web server, database table, playfab api, or any method appropriate.
        if (databaseManager.CheckDatabase(msg.username, msg.password)){
            // proceed to regular add player
            Debug.Log("Matching Credentials");
            base.OnServerAddPlayer(connection, extraMessage);
        }
        else{
            connection.Disconnect();
        } 
    }

    // I have no idea why I can't call StartClient() 
    // on a button so this works xD
    public void StartClientClient() {
        base.StartClient();
    }
}
