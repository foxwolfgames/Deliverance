using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyRoot : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Animator animator;

    [Header("Stats")]
    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
}
