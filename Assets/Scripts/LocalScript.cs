using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScript : MonoBehaviour
{   
    public Animator animator; 
    public new Rigidbody rigidbody;
    public Plane centerPlane;
    public float speed;

    private void Awake() {
        centerPlane = new Plane(Vector3.up, -1.5f);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Swing");
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

    private void LookAtRaycast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (centerPlane.Raycast(ray, out enter)) {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 1.5f;
            this.transform.LookAt(hitPoint);
        }
    }
}
