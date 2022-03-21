using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 3;
    public float speed = 3f;
    public int coins = 1;
    public List<Transform> waypoints;
    public ManagerScript manager;
    
    private Transform t;
    private int targetIndex;
    private Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        GameObject waypointParent = GameObject.Find("Waypoints");
        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child.transform);
        }

        t = GetComponent<Transform>();
        t.position = waypoints[0].position;
        targetIndex = 1;
        targetDirection = waypoints[1].position - t.position;

        manager = GameObject.Find("GameManager").GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        Vector3 target = waypoints[targetIndex].position;
        Vector3 movementDir = (target - t.position).normalized;
        t.position = t.position + (movementDir * speed * Time.deltaTime);

        //check if past waypoint
        if (Vector3.Dot(movementDir, targetDirection) <= 0)
        {
            if (targetIndex == waypoints.Count - 1)
            {
                //enemy made it through the gauntlet
                manager.subtractLives(1);
                Destroy(this.gameObject);
            }
            else
            {
                targetIndex++;
                targetDirection = waypoints[targetIndex].position - t.position;
            }
        }

        //check health
        if (health <= 0)
        {   
            manager.addCoins(coins);
            Destroy(this.gameObject);
        }
    }

    public void subtractHealth(int x)
    {
        health -= x;
    }
}
