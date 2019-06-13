using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : NetworkBehaviour {

    private CameraController cameraController;
    public Transform statBarsCanvas;
    public Transform healthBar;
    public Transform player;
    public Vector3 offset;

    private void Awake() {
        cameraController = GetComponent<CameraController>();
    }

    private void Start() {
        if(!isLocalPlayer) {
            statBarsCanvas = ClientScene.localPlayer.GetComponent<UIManager>().statBarsCanvas;
            healthBar.SetParent(statBarsCanvas);
            return;
        }

        statBarsCanvas.SetParent(null);
    }

    public void SetUI() {
        healthBar.position = cameraController.mainCamera.WorldToScreenPoint(player.position) + offset;    
    }

    /*
        When the player disconnects dispose of anything that isnt a child 
    */
    private void OnDestroy() {
        cameraController.localController.otherUIManager.Remove(this);
        Destroy(healthBar.gameObject);
    }
}
