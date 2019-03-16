using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    [Range(0,100)]
    public float speed;
    public TextMeshProUGUI xInputText;
    public TextMeshProUGUI zInputText;
    public TextMeshProUGUI velocityText;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical"); 
        xInputText.text = x.ToString();
        zInputText.text = z.ToString();

        Vector3 direction = new Vector3(x, 0 ,z);
        rigidbody.velocity = direction * speed;
        velocityText.text = rigidbody.velocity.ToString();
    }   
}
