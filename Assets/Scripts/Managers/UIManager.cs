using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class UIManager : NetworkBehaviour {

    private CameraController cameraController;
    public RectTransform healthBar;
    public Transform player;
    public Vector3 offset;

    private void Awake() {
        cameraController = GetComponent<CameraController>();
    }

    private void Start() {
        offset = healthBar.position - cameraController.mainCamera.WorldToScreenPoint(player.position);
    }

    private void FixedUpdate() {
        healthBar.position = cameraController.mainCamera.WorldToScreenPoint(player.position) + offset;          
    }

}
