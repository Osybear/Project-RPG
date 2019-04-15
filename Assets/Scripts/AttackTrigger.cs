using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{   
     public PlayerManager playerManager;
     public Transform pivot;
     public float swingTime;
     public float swingAngle;

     private void Awake() {
          pivot = transform.parent;
          pivot.Rotate(0, 60, 0);
          GetComponent<BoxCollider>().enabled = false;     
     }

     private void Start() {
          StartCoroutine(RotatePivotOverTime());
     }   

     public IEnumerator RotatePivotOverTime() {
          float elapsedTime = 0;
          while(elapsedTime < swingTime) {
               pivot.transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(swingAngle, -swingAngle, (elapsedTime/swingTime)), 0);
               elapsedTime += Time.deltaTime;
               if(pivot.transform.localRotation.y < 0f)
                    GetComponent<BoxCollider>().enabled = true;  
               yield return new WaitForEndOfFrame();
          }
          playerManager.isAttacking = false;
          Destroy(transform.parent.gameObject);
     }

     private void OnTriggerEnter(Collider other) {
          if(!other.gameObject.name.Equals("Local Player")) {
               Debug.Log("Something was hit");
               StopAllCoroutines();
               playerManager.isAttacking = false;
               Destroy(transform.parent.gameObject); 
          }
     }
}
