using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour 
{
    public new Rigidbody rigidbody;
    public Animator animator; 
    [Range(0,100)]
    public float speed;
    public Plane centerPlane;
    
    #region Refactor Components Here
        private IMovementInput movementInput;
        private MovementDirection movementDirection;    
    #endregion

    private void Awake() {
        #region Get Components of interface
            movementInput = GetComponent<IMovementInput>();
            movementDirection = GetComponent<MovementDirection>();
        #endregion

        //creating a plane to then later use with a raycast
        centerPlane = new Plane(Vector3.up, -1.5f);
    }

    private void Start() {
        //checking if the current network is the local player
        //the localplayer if statement doesnt need to be refactored
        //because this script impliments NetworkBehavior
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
        //I already split up the behaviour here 
        PlayerMovement();
        LookAtRaycast();
    }

    //managing player movement
    private void PlayerMovement() {
        //getting input from the unity input manager
        float horizontal = movementInput.GetHorizontal();
        float vertical = movementInput.GetVertical();
        
        //determining global and local direction the player wants to go using player input
        Vector3 globalDirection = movementDirection.GetGlobalMovementDirection(horizontal, vertical);
        Vector3 localDirection = movementDirection.GetLocalMovementDirection(transform, globalDirection);

        //using direction determination and using it to apply movement using a rigid body
        rigidbody.velocity = globalDirection * speed;


        //managing animation states using direction data
        animator.SetFloat("Horizontal", localDirection.x);
        animator.SetFloat("Vertical", localDirection.z);
        animator.SetFloat("MotionSpeed", speed);
        animator.SetFloat("Velocity", rigidbody.velocity.magnitude);
    }

    public void LookAtRaycast() {
        //creating a ray 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //then testing that ray againts the plane created 
        float enter = 0.0f;
        if (centerPlane.Raycast(ray, out enter)) {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 1.5f;
            this.transform.LookAt(hitPoint);
        }
    }   


}

public class PlayerMovementDirection : MonoBehaviour, IMovementDirection
{
    public Vector3 GetGlobalMovementDirection(float horizontal, float vertical) {
        return new Vector3(horizontal, 0, vertical);
    }

    public Vector3 GetLocalMovementDirection(Transform player, Vector3 globalDirection) {
        return player.transform.InverseTransformDirection(globalDirection);
    }
}

internal interface IMovementDirection
{
    Vector3 GetGlobalMovementDirection(float horizontal, float vertical);
    Vector3 GetLocalMovementDirection(Transform player, Vector3 globalDirection);
}