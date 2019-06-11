using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//https://stackoverflow.com/questions/46003824/sending-http-requests-in-c-sharp-with-unity

public class DatabaseManager : MonoBehaviour {   
    private static string accountUrl = "https://xnr.gg/nVcihgr8Mo/";
    
    private void Start() {
        //StartCoroutine(PostRequestFormAccount("Osybear", "password"));
    }

    public static IEnumerator PostRequestFormAccount(System.Action<string> callback, string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField("action0", "doval");
        form.AddField("username", username);
        form.AddField("password", password);
        //Debug.Log("Username : " + username);
        //Debug.Log("Password : " + password);

        UnityWebRequest uwr = UnityWebRequest.Post(accountUrl, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            callback(uwr.downloadHandler.text);
        }
    }
}
