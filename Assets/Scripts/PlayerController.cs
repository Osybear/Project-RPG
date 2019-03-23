using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerController : NetworkBehaviour {
    private new Rigidbody rigidbody;
    [Range(0,100)]
    public float speed;
    public TextMeshProUGUI xInputText;
    public TextMeshProUGUI zInputText;
    public TextMeshProUGUI velocityText;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Start() {
        if(!isLocalPlayer)
            transform.GetChild(0).gameObject.SetActive(false); 
    }

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
    }   
}
