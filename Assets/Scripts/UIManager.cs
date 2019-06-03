using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class UIManager : NetworkBehaviour {
    public Camera mainCamera;
    public RectTransform healthBar;
    public Transform player;
    public Vector3 offset;
    
    private void Start() {
        if(!isLocalPlayer) {
            // on the server this will return null since there is no local player main camera activated
            mainCamera = Camera.main;
        }

        if(mainCamera != null)
            offset = healthBar.position - mainCamera.WorldToScreenPoint(player.position);
    }

    private void FixedUpdate() {
        if(mainCamera != null)
            healthBar.position = mainCamera.WorldToScreenPoint(player.position) + offset;          
    }

}
