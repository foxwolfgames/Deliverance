using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int HP;
    private EnemyRoot enemyRoot;
    private float attackRange;
    private float pathUpdateDeadline;

    
    private void Awake()
    {
        enemyRoot = GetComponent<EnemyRoot>();
    }
    void Start()
    {
        attackRange = enemyRoot.navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if (target != null) {
            bool inRange = Vector3.Distance(target.position, transform.position) <= attackRange;
            if (inRange) {
                LookAtTarget();
            } else {
                UpdatePath();
            }

            // enemyRoot.animator.SetBool("attacking", inRange);
        }
        // enemyRoot.animator.SetFloat("speed", enemyRoot.navMeshAgent.desiredVelocity.sqrMagnitude);
    }
    
    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline) {
            pathUpdateDeadline = Time.time + enemyRoot.pathUpdateDelay;
            enemyRoot.navMeshAgent.SetDestination(target.position);
        }
    }

    public void Attack()
    {
        print("Attacking");
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0) {
            // enemyRoot.animator.SetTrigger("DIE");
        }
        else 
        {
            // enemyRoot.animator.SetTrigger("DAMAGE");
            print("Took damage: " + damageAmount);
            print("HP: " + HP);
        }
    }
    
}
