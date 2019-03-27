using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : NetworkBehaviour {
    public Camera mainCamera;
    public RectTransform healthBar;
    public Transform player;

    private void Start() {
        if(!isLocalPlayer) {
            mainCamera.gameObject.SetActive(false);
            mainCamera = Camera.main;
        }    
    }

    private void Update() {
        if(mainCamera != null)
            healthBar.position = mainCamera.WorldToScreenPoint(player.position);          
    }
}
