using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemySpawn;
    public int maxEnemy;
    public float timeBetweenSpawns;
    private int enemyCount = 0;
    private int levelCount = 1;
    private bool lastWasIncrease;

    public GameObject LevelUI;
    public Health hp;

    void Start()
    {
        InvokeRepeating("Spawn", 0, timeBetweenSpawns);
        hp = GameObject.Find("Health").GetComponent<Health>();
    }

    void Update()
    {
        if (GameObject.Find("EnemyZ(Clone)") == null)
        {
            lastWasIncrease = false;
            Level(true);
        }

        LevelUI.GetComponent<Text>().text = "" + levelCount;
    }

    void Spawn()
    {
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 1.5f, Random.Range(-7.5f, 14.0f));
        Instantiate(enemySpawn, position, Quaternion.identity);

        enemyCount++;
        if (enemyCount >= maxEnemy)
        {
            StartCoroutine(LevelManage());
        }
    }

    IEnumerator LevelManage()
    {
        CancelInvoke("Spawn");
        yield return new WaitForSeconds(5);
        enemyCount = 0;
        maxEnemy++;
       
    }

    void Level(bool inc)
    {
        if(inc && !lastWasIncrease)
        {
            levelCount++;
            hp.playerHealth = 100;
            lastWasIncrease = true;
            InvokeRepeating("Spawn", 0, timeBetweenSpawns);
        }
        
    }

}
