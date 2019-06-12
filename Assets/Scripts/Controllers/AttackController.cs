using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class AttackController : NetworkBehaviour
{
    public LayerMask layerMask;
    public float radius;
    public float maxAngle;
    public int damage;

    private void Update() {
        if(!isLocalPlayer)
            return;

        if(Input.GetMouseButtonDown(0)) {
            CmdSwingAttack();
        }
    }
    
    [Command]
    private void CmdSwingAttack() {
        GameObject destructible = GetDestructibleObject();

        if(destructible == null)
            return;

        Health health = destructible.GetComponent<Health>();
        if(health == null) {
            Debug.Log("Object : " + destructible.name + " does not have a health component. ");
            return;
        }

        Debug.Log("Destructable Object : " + destructible.name);
        Debug.Log("Health Component : " + health);
        health.OnDamage(damage);
    }

    [Server]
    private GameObject GetDestructibleObject() {
        Collider[] results = new Collider[25];
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
        GameObject nearestObj = null;
        float nearestEnemyAngle = float.MaxValue;

        for(int i = 0; i < hits; i++) {
            GameObject destructibleObj = results[i].transform.gameObject;
            if(destructibleObj.Equals(gameObject))
                continue;

            float a = Vector3.Distance(transform.position + (transform.forward * radius), destructibleObj.transform.position);
            float b = radius;
            float c = Vector3.Distance(transform.position, destructibleObj.transform.position);
            float angle = Mathf.Acos(((b * b) + (c * c) - (a * a)) / (2 * b * c)) * Mathf.Rad2Deg;

            if(angle < nearestEnemyAngle) {
                nearestObj = destructibleObj;
                nearestEnemyAngle = angle;
            }
        }

        if(nearestObj == null || nearestEnemyAngle > maxAngle)
            return null; 

        return nearestObj;
    }

    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
