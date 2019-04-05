using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerManager : NetworkBehaviour {

    public new Camera camera;
    public new Rigidbody rigidbody;
    [Range(0,100)]
    public float speed;
    public TextMeshProUGUI xInputText;
    public TextMeshProUGUI zInputText;
    public TextMeshProUGUI velocityText;
    public Plane lookAtPlane;
    public GameObject testCube;

    private void Awake() {
        lookAtPlane = new Plane(Vector3.up, -1.5f);    
    }

    private void Start() {
        if(!isLocalPlayer) {    
            gameObject.name = "Non Local Player";   
        }else {
            gameObject.name = "Local Player";   
        }
    }

    /*
        Have the player gameobject look towards the mouseposition
        but only have rotate on the Y axis
     */
    private void Update() {
        if(!isLocalPlayer)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical"); 
        xInputText.text = x.ToString();
        zInputText.text = z.ToString();

        Vector3 direction = new Vector3(x, 0 ,z);
        rigidbody.velocity = direction * speed;
        velocityText.text = rigidbody.velocity.ToString();

        //Detect when there is a mouse click
        if (Input.GetMouseButton(0))
        {
            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Initialise the enter variable
            float enter = 0.0f;

            if (lookAtPlane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);
                hitPoint.y = 1.5f;
                //Move your cube GameObject to the point where you clicked
                testCube.transform.position = hitPoint;
            }
        }
    }   
}
