using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour 
{   
    private CameraController cameraController;
    public new Rigidbody rigidbody;
    public Animator animator; 
    [Range(0,100)]
    public float speed;
    public Plane centerPlane;
    
    private void Awake() {
        cameraController = GetComponent<CameraController>();
        centerPlane = new Plane(Vector3.up, -1.5f);
    }

    private void Start() {
        if(!isLocalPlayer) {    
            gameObject.name = "Non Local Player";   
        }else {
            gameObject.name = "Local Player";
        }
    }

    private void Update() {
        if(!isLocalPlayer)
            return;
        if(Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");
        }
        PlayerMovement();
        LookAtRaycast();
    }

    private void PlayerMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); 
        
        Vector3 globalDirection = new Vector3(horizontal, 0, vertical);
        rigidbody.velocity = globalDirection * speed;
        Vector3 localDirection = transform.InverseTransformDirection(globalDirection);

        animator.SetFloat("Horizontal", localDirection.x);
        animator.SetFloat("Vertical", localDirection.z);
        animator.SetFloat("MotionSpeed", speed);
        animator.SetFloat("Velocity", rigidbody.velocity.magnitude);
    }

    public void LookAtRaycast() {
        Ray ray = cameraController.mainCamera.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (centerPlane.Raycast(ray, out enter)) {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 1.5f;
            this.transform.LookAt(hitPoint);
        }
    }   
}
