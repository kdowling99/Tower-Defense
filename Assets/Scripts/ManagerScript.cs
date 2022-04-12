using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public int coins = 2;
    public TextMeshProUGUI livesText;
    public int lives = 1;
    public GameObject enemy;
    public int enemyCount = 3;
    public GameObject enemyParent;
    public GameObject tower;

    public GameObject button;
    public TextMeshProUGUI buttonText;

    public void refresh()
    {
        GameObject[] perishables = GameObject.FindGameObjectsWithTag("delete");
        foreach (GameObject thing in perishables)
        {
            Destroy(thing);
        }

        coins = 2;
        addCoins(0);
        lives = 1;
        subtractLives(0);
        StartCoroutine(spawnEnemies(enemyCount));
        StartCoroutine(disableButton());
    }

    IEnumerator disableButton()
    {
        yield return new WaitForSeconds(0.25f);
        button.SetActive(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name.Contains("Platform"))
                {
                    if (coins >= 2)
                    {
                        GameObject currentTower = GameObject.Instantiate(tower);
                        currentTower.transform.SetParent(hit.collider.transform);
                        currentTower.transform.position = new Vector3(hit.collider.transform.position.x,
                                                                      currentTower.transform.position.y,
                                                                      hit.collider.transform.position.z);
                        addCoins(-2);
                    }
                }
            }
        }

        //FindFarObjects();

        if (lives <= 0)
        {
            buttonText.SetText("Restart");
            button.gameObject.SetActive(true);
        }
    }

    public void addCoins(int x)
    {
        coins += x;
        coinsText.SetText("COINS\n" + coins.ToString("D2"));
    }

    public void subtractLives(int x)
    {
        lives -= x;
        livesText.SetText("LIVES\n    " + lives);
    }

    IEnumerator spawnEnemies(int enemyCount)
    {
        for(int i=0; i<enemyCount; i++)
        {
            GameObject currentEnemy = GameObject.Instantiate(enemy);
            currentEnemy.GetComponent<Transform>().SetParent(enemyParent.transform);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }


    //for debug
    public static void FindFarObjects()
    {
        List<GameObject> farObjs = new List<GameObject>();
        var allObjs = GameObject.FindObjectsOfType<GameObject>();
        for (var i = 0; i < allObjs.Length; i++)
        {
            if ((Mathf.Abs(allObjs[i].transform.position.x) > 1000) ||
                (Mathf.Abs(allObjs[i].transform.position.y) > 500) ||
                (Mathf.Abs(allObjs[i].transform.position.z) > 1000)
            )
            {
                farObjs.Add(allObjs[i]);
            }
        }

        if (farObjs.Count > 0)
        {
            for (var i = 0; i < farObjs.Count; i++)
            {
                Debug.LogError($"Found object {farObjs[i].name} at location {farObjs[i].transform.position}");
            }
        }
        else
        {
            Debug.Log("No Far objects");
        }
    }

}
