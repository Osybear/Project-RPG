using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : NetworkBehaviour {

    private CameraController cameraController;
    public Transform healthBar;
    public Transform player;
    public Vector3 offset;

    private void Awake() {
        cameraController = GetComponent<CameraController>();
    }

    public void SetUI() {
        healthBar.position = cameraController.mainCamera.WorldToScreenPoint(player.position) + offset;    
    }

    private void LateUpdate() {
        //SetUI();
    }
}
