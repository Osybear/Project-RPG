using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used on the world canvas so that 
    the health bar is facing the camera
 */
public class LookAtCamera : MonoBehaviour
{
    public Camera m_Camera;
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate() {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}
