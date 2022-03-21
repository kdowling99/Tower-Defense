using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public int coins = 0;
    public TextMeshProUGUI livesText;
    public int lives = 1;
    public GameObject enemy;
    public int enemyCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        addCoins(0);
        subtractLives(0);
        StartCoroutine(spawnEnemies(enemyCount));
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject.Instantiate(enemy);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }
}
