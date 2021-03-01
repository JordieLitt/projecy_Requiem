using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vision : MonoBehaviour
{
    public Transform target;

    public NavMeshAgent agent;

    public LayerMask whatIsGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float GuardDistance = 5.0f;

    //prints "close" if the z-axis of this transform looks
    //almost towards the target

    private void Awake()
    {
        target = GameObject.Find("Main_Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(angle <= 75.0f)
        {
            if(distance <= GuardDistance)
            {
                agent.SetDestination(target.position);
                print("chasing");
            }
            else
            {
                print("out of range");
                Patrolling();
            }

            if(Physics.Raycast(transform.position,(forward), out hit))
            {
                if(hit.transform.tag == "Boxes" && Input.GetKeyDown(KeyCode.X))
                {
                    Patrolling();
                    print("box in way");
                }
            }
        }

        else
        {
            print("far");
            Patrolling();
        }
    }

     private void Patrolling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
