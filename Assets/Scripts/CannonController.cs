using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform rotater;
    public Transform enemyParent;
    public float range = 10f;
    public float shotTimer = 1f;

    private GameObject targetEnemy;
    private bool canShoot;
    private float timeSinceLastShot;
    public GameObject cannonball;
    // Start is called before the first frame update
    void Start()
    {
        rotater = this.transform.GetChild(3);
        enemyParent = GameObject.Find("Enemies").transform;
        targetEnemy = null;
        canShoot = true;
        timeSinceLastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy == null)
        {
            //find another target
            int i = 0;
            while (i<enemyParent.childCount && targetEnemy == null)
            {
                
                GameObject possibleTarget = enemyParent.GetChild(i).gameObject;
                if (Vector3.Distance(this.transform.position, possibleTarget.transform.position) < range)
                    targetEnemy = possibleTarget;
                i++;
            }
        } else
        {
            rotater.LookAt(targetEnemy.transform);
            //rotater.Rotate(0, rotater.rotation.y, rotater.rotation.z);   FIX ME
            if (Vector3.Distance(this.transform.position, targetEnemy.transform.position) < range)
            {
                // shoot
                if (canShoot)
                {
                    GameObject shot = GameObject.Instantiate(cannonball);
                    shot.transform.SetParent(this.transform);
                    shot.GetComponent<CannonballScript>().setTarget(targetEnemy.transform);
                    canShoot = false;
                    timeSinceLastShot = 0;
                } else
                {
                    if (timeSinceLastShot > shotTimer)
                    {
                        canShoot = true;
                    } else
                    {
                        timeSinceLastShot += Time.deltaTime;
                    }
                }
                
            } else
            {
                targetEnemy = null;
            }
        }
    }
}
