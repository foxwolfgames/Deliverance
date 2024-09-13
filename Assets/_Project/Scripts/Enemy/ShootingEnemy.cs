using System;
using System.Collections;
using System.Collections.Generic;
using FWGameLib.Common.StateMachine;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public Transform target;
    private EnemyRoot enemyRoot;
    private StateMachine stateMachine;
    private float attackRange;
    private float pathUpdateDeadline;

    [Header("General")]
    public Transform shootPoint; // Where the raycast starts from
    public Transform gunPoint; // Where this visual trail starts from
    public LayerMask layerMask;

    [Header("Gun")]
    public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f); // How much spread the gun has
    public TrailRenderer bulletTrail;
    
    void Start()
    {
        enemyRoot = GetComponent<EnemyRoot>();
        
        stateMachine = new StateMachine();

        // STATES (mayube for spitter -> hide, shoot, run)

        // TRANSITIONS

        // START STATE

        // FUNCTIONS & CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddTransition(null, to, condition);
    }

    void Update()
    {
        stateMachine.Tick();
        if (target != null) {
            bool inRange = Vector3.Distance(target.position, transform.position) <= attackRange;
            if (inRange) {
                LookAtTarget();
                Shoot();
            } else {
                UpdatePath();
            }

            // enemyRoot.animator.SetBool("attacking", inRange);
        }
        // enemyRoot.animator.SetFloat("speed", enemyRoot.navMeshAgent.desiredVelocity.sqrMagnitude);
    }
    
    private void OnDrawGizmos()
    {
        if (stateMachine!= null) {
            // Gizmos.color = stateMachine.GetGizmoGolor();  <- this should allow us to visualize the current state, but it's not working right now
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
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

    public void Shoot()
    {
        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask)) {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            TrailRenderer trail = Instantiate(bulletTrail, gunPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
        }
    }

    public Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        direction += new Vector3(
            UnityEngine.Random.Range(-spread.x, spread.x), 
            UnityEngine.Random.Range(-spread.y, spread.y), 
            UnityEngine.Random.Range(-spread.z, spread.z)
        );
        direction.Normalize();
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        // object pooling to refactor this code
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }
}
