using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateToPoint : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

