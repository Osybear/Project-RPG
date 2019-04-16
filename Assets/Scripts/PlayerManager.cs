using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour 
{
    public new Camera camera;
    public new Rigidbody rigidbody;
    [Range(0,100)]
    public float speed;
    public TextMeshProUGUI xInputText;
    public TextMeshProUGUI zInputText;
    public TextMeshProUGUI velocityText;
    public Plane centerPlane;
    public GameObject attackTrigger;
    public bool isAttacking;

    private void Awake() {
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
        PlayerMovement();
        LookAtRaycast();
        Swing();
    }

    public void PlayerMovement() {  
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical"); 
        xInputText.text = x.ToString();
        zInputText.text = z.ToString();

        Vector3 direction = new Vector3(x, 0 ,z);
        rigidbody.velocity = direction * speed;
        velocityText.text = rigidbody.velocity.ToString();
    }

    public void LookAtRaycast() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (centerPlane.Raycast(ray, out enter)) {
            //Get the point that is clicked
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = 1.5f;
            //Move your cube GameObject to the point where you clicked
            this.transform.LookAt(hitPoint);
        }
    }   
 
    public void Swing() {
        if(Input.GetMouseButtonDown(0) && !isAttacking) {
            isAttacking = true;
            GameObject trigger = Instantiate(attackTrigger, this.transform, false);
            trigger.transform.GetChild(0).GetComponent<AttackTrigger>().playerManager = this;
        }
    }
}
