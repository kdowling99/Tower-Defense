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

    // Start is called before the first frame update
    void Start()
    {
        addCoins(2);
        subtractLives(0);
        StartCoroutine(spawnEnemies(enemyCount));
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
}
