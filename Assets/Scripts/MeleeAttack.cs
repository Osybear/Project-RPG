using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeleeAttack : MonoBehaviour
{
    public LayerMask layerMask;
    public float radius;
    public float maxAngle;

    private void Update() {
        //if(!isLocalPlayer)
        //    return;
        SwingAttack();
    }

    private void SwingAttack() {
        if(!Input.GetMouseButtonDown(0))
            return;
        Debug.Log("Swing Attack");
        GameObject enemy = GetNearestEnemy();
        
        if(enemy == null)
            return;
        
        Debug.Log("Enemy Hit : " + enemy.name);
    }

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
            if(nearestEnemy == null || angle < nearestEnemyAngle) {
                nearestEnemy = enemy;
                nearestEnemyAngle = angle;
            }
        }
        Debug.Log("Angle : " + nearestEnemyAngle);
        if(nearestEnemy == null || nearestEnemyAngle > maxAngle)
            return null; 
        Debug.Log("Nearest Enemy Angle : " + nearestEnemyAngle);
        return nearestEnemy;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
