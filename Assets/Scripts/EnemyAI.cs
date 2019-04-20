using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask layerMask;
    public float radius;

    private void Update() {
        
    }

    private GameObject GetNearestPlayer() {
        Collider[] results = new Collider[25];
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);

        for(int i = 0; i < hits; i++) {
            GameObject player = results[i].transform.gameObject;
            
        }

        return nearestEnemy;
    }
}
