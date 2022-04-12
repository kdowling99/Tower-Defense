using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playercontroller : MonoBehaviour
{
    public NavMeshAgent player;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 navMeshPosition = ;
        player.SetDestination(target.position);
    }
}
