using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyRoot : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    // [HideInInspector] public Animator animator;

    [Header("Stats")]
    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
    }
}
