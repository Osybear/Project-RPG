using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : NetworkBehaviour {
    public Camera mainCamera;
    public RectTransform healthBar;
    public Transform player;

    private void Update() {
        if(!isLocalPlayer) {
            healthBar.position = mainCamera.WorldToScreenPoint(player.position);          
        }
    }
}
