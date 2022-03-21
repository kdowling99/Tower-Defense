using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Enemy(Clone)")
                {
                    hit.collider.gameObject.GetComponent<EnemyScript>().subtractHealth(1);
                }
            }
        }
        
    }
}
