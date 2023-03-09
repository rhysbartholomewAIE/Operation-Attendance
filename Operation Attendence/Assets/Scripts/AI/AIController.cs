using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AIController : MonoBehaviour
{


    private NavMeshAgent agent;
    public float allowanceRange;
    private bool active = true;

    private Animator animator;
    
    public List<Transform> patrolPointList;
    private Vector3 currentPatrolPoint;
    public float walkRadius = 50f;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewPatrolTarget();
        animator = GetComponentInChildren<Animator>();
    }

    public void FindNewPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        randomDirection += transform.position;
        NavMeshHit hitNav;
        NavMesh.SamplePosition(randomDirection, out hitNav, walkRadius, 1);
        Vector3 finalPosition = hitNav.position;
        currentPatrolPoint = finalPosition;
        Invoke("FindNewPatrolTarget", Random.Range(8, 15));
    }

    private void Update()
    {
        if (!active) return;
        if (currentPatrolPoint == null)
        {
            FindNewPatrolTarget();
            if (!active)
                return;
        }
        if (Vector3.Distance(transform.position, currentPatrolPoint) > allowanceRange)
        {
            agent.SetDestination(currentPatrolPoint);

            if (animator.GetFloat("Speed") < 0.95)
            {
                float speed = animator.GetFloat("Speed");
                speed += 0.01f;
                animator.SetFloat("Speed", speed);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
            if (animator.GetFloat("Speed") > 0)
            {
                float speed = animator.GetFloat("Speed");
                speed -= 0.01f;
                animator.SetFloat("Speed", speed);
            }

            transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
        }           
    }
}

