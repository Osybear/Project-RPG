using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
//https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html

public class LogIn : MonoBehaviour
{   
    public NetworkManager networkManager;
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    private void Awake() {
        networkManager = GameObject.FindObjectOfType<NetworkManager>();
        usernameField.text = "Osybear";
        passwordField.text = "password";
    }

    public void Connect() {
        StartCoroutine(DatabaseManager.PostRequestFormAccount((result) => {
            if(result.Equals("true")) {
                networkManager.StartClient();
            }else {
                Debug.Log("Incorrect Username/Password");
            }
        }, usernameField.text, passwordField.text));    
    }
}
