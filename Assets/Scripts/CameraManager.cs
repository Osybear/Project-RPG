using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    public Transform player;
    public Vector3 offset;

    private void Start () {
        player = this.transform.parent;
        this.transform.parent = null;
        offset = transform.position - player.transform.position;
    }
    
    private void LateUpdate () {
        transform.position = player.transform.position + offset;
    }
}
