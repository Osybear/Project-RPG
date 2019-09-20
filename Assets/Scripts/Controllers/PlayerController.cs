using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour 
{   
    private CameraController cameraController;
    private Rigidbody myRigidbody;
    private Animator animator; 

    [Range(0,100)]
    public float speed;
    public Plane centerPlane;
    
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        cameraController = GetComponent<CameraController>();
        centerPlane = new Plane(Vector3.up, -1.5f);
    }

    private void Start() {  
        if(!isLocalPlayer) {    
            gameObject.name = "Non Local Player";
            myRigidbody.constraints = RigidbodyConstraints.FreezeAll;   
        }else {
            gameObject.name = "Local Player";
        }
    }

    private void Update() {
        if(!isLocalPlayer)
            return;
        
        PlayerMovement();
        LookAtRaycast();
    }

    private void PlayerMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); 
        
        Vector3 globalDirection = new Vector3(horizontal, 0, vertical);
        myRigidbody.velocity = globalDirection * speed;
        Vector3 localDirection = transform.InverseTransformDirection(globalDirection);

        animator.SetFloat("Horizontal", localDirection.x);
        animator.SetFloat("Vertical", localDirection.z);
        animator.SetFloat("MotionSpeed", speed);
        animator.SetFloat("Velocity", myRigidbody.velocity.magnitude);
    }

    private void LookAtRaycast() {
        Ray ray = cameraController.mainCamera.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (centerPlane.Raycast(ray, out enter)) {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 1.5f;
            this.transform.LookAt(hitPoint);
        }
    }


}
