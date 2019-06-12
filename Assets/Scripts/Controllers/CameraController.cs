using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{   
    public Camera mainCamera;
    public Vector3 offset;
    public CameraController localController;

    private void Start() {
        if(!isLocalPlayer) {
            mainCamera.gameObject.SetActive(false);
            localController = ClientScene.localPlayer.GetComponent<CameraController>();
            mainCamera = localController.mainCamera;
            return;
        } 

        offset = mainCamera.transform.position - transform.position;
        mainCamera.transform.parent = null;
    }

    private void LateUpdate () {
        if(!isLocalPlayer)
            return;
            
        mainCamera.transform.position = transform.position + offset;
    }

}
