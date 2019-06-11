using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{   
    public new Camera camera;
    public Vector3 offset;

    private void Start () {
        camera.transform.parent = null;
        offset = camera.transform.position - transform.position;

        setLocalCamera();
    }
        
    private void LateUpdate () {
        camera.transform.position = transform.position + offset;
    }

    private void setLocalCamera() {
        if(!isLocalPlayer) {
            camera.gameObject.SetActive(false);
        }    
    }
}
