using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    public Transform target = null;
    public float speed;
    public Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position += (target.position - transform.position).normalized * Time.deltaTime * speed;
            if ((target.position - transform.position).magnitude < 1)
            {
                hit();
            }
        }
    }

    public void setTarget (Transform t)
    {
        target = t;
    }

    public void hit()
    {
        //Debug.Log("HIT");
        target.GetComponent<EnemyScript>().subtractHealth(1);
        Destroy(this.gameObject);
    }
}
