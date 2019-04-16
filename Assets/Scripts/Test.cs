using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask layerMask;
    public List<GameObject> enemiesHit;
    public float radius;
    public GameObject nearestEnemy;
    public float nearestEnemyAngle;

    private void Start() {
        Debug.Log(Vector3.Distance(transform.position, nearestEnemy.transform.position));
        Debug.Log(Vector3.Distance(new Vector3(0, 0, 5), new Vector3(5, 0, 0)));    
    }

    private void Update() {
        nearestEnemy = null;
        enemiesHit.Clear();
        Collider[] results = new Collider[10];
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
        for(int i = 0; i < hits; i++) {
            GameObject enemy = results[i].transform.gameObject;
            float a = Vector3.Distance(transform.forward * radius, enemy.transform.position);
            float b = radius;
            float c = Vector3.Distance(transform.position, enemy.transform.position);
            //cos A = (b2 + c2 − a2) / 2bc
            float angle = Mathf.Acos(((b * b) + (c * c) - (a * a)) / (2 * b * c)) * Mathf.Rad2Deg;

            if(nearestEnemy == null || angle < nearestEnemyAngle) {
                nearestEnemy = enemy;
                nearestEnemyAngle = angle;
            }

            enemiesHit.Add(enemy);
        } 
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
