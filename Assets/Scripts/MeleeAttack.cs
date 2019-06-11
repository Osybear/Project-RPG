using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MeleeAttack : NetworkBehaviour
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
        GameObject enemy = GetNearestEnemy();

        if(enemy == null)
            return;

        Health health = enemy.GetComponent<Health>();
        if(health == null)
            return;

        Debug.Log("Enemy Name : " + enemy.name);
        Debug.Log("Health Component : " + health);
        health.TakeDamage(damage);
    }

    [Server]
    private GameObject GetNearestEnemy() {
        Collider[] results = new Collider[25];
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
        GameObject nearestEnemy = null;
        float nearestEnemyAngle = float.MaxValue;

        for(int i = 0; i < hits; i++) {
            GameObject enemy = results[i].transform.gameObject;
            float a = Vector3.Distance(transform.position + (transform.forward * radius), enemy.transform.position);
            float b = radius;
            float c = Vector3.Distance(transform.position, enemy.transform.position);
            float angle = Mathf.Acos(((b * b) + (c * c) - (a * a)) / (2 * b * c)) * Mathf.Rad2Deg;
            if(enemy != this.gameObject || nearestEnemy == null || angle < nearestEnemyAngle) {
                nearestEnemy = enemy;
                nearestEnemyAngle = angle;
            }
        }

        if(nearestEnemy == null || nearestEnemyAngle > maxAngle)
            return null; 

        return nearestEnemy;
    }

    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
