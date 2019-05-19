using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScript : MonoBehaviour
{   
    public Animator animator; 
    public new Rigidbody rigidbody;
    public float speed;

    void Update() {
        float vertical = Input.GetAxisRaw("Vertical"); 
        float horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);

        Vector3 direction = new Vector3(vertical, 0, horizontal);
        rigidbody.velocity = direction * speed;

        animator.SetFloat("Velocity", rigidbody.velocity.magnitude);
    }
}
