using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[RequireComponent(typeof(IFollowTarget), typeof(ISetParent))]
public class CameraController : MonoBehaviour
{   
    private IFollowTarget followTarget;
    private ISetParent setParent;
    private Camera mainCamera;
    private GameObject player;

    private void Awake() {
        player = this.gameObject;
        mainCamera = GetComponentInChildren<Camera>();
        followTarget = GetComponent<IFollowTarget>();
        setParent = GetComponent<ISetParent>();
    }

    private void Start () {
        setParent.Set(mainCamera.transform);
        followTarget.SetTransformOffset(mainCamera.transform, player.transform);
    }
        
    private void LateUpdate () {  
        followTarget.SetTransformPosition(mainCamera.transform, player.transform);
    }
}
