using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{      
    private UIManager UIManager;
    public Camera mainCamera;
    public Vector3 offset;
    public CameraController localController;

    public List<UIManager> otherUIManager; // nonlocal UIManager 

    private void Awake() {
        UIManager = GetComponent<UIManager>();    
    }

    private void Start() {
        if(isServer)
            return;

        if(!isLocalPlayer) {
            mainCamera.gameObject.SetActive(false);
            localController = ClientScene.localPlayer.GetComponent<CameraController>();
            mainCamera = localController.mainCamera;
            localController.otherUIManager.Add(GetComponent<UIManager>());
            return;
        } 

        offset = mainCamera.transform.position - transform.position;
        mainCamera.transform.SetParent(null);
    }

    private void LateUpdate () {
        if(!isLocalPlayer)
            return;
            
        mainCamera.transform.position = transform.position + offset;

        UIManager.SetUI();
        foreach (UIManager manager in otherUIManager)
        {
            manager.SetUI();
        }
    }
}
