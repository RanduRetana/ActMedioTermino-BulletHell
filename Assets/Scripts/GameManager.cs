using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject Boss;
    private int enemyCount;
    private int enemiesDestroyed = 0;
    private bool bossSpawned = false;
    public static int bulletCount = 0;
    // Start is called before the first frame update
    void Start()
    {
    SpawnEnemy();
    TextMeshProUGUI bulletText = GameObject.Find("BulletCount").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI bulletText = GameObject.Find("BulletCount").GetComponent<TextMeshProUGUI>();
        bulletText.text = "Bullets: " + bulletCount;
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-60, 60), 0, Random.Range(-10, 10)), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-30, 30), 0, Random.Range(-5, 5)), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-60, 60), 0, Random.Range(-10, 10)), Quaternion.identity);
        Instantiate(enemy, new Vector3(Random.Range(-30, 30), 0, Random.Range(-5, 5)), Quaternion.identity);
    }
    void SpawnBoss()
    {
        GameObject boss = Instantiate(Boss, new Vector3(Random.Range(-60, 60), 0, Random.Range(-10, 10)), Quaternion.identity);
        boss.SetActive(true);
    }
    public void EnemyDestroyed()
    {
        enemiesDestroyed++;

        if (enemiesDestroyed >= 5 && !bossSpawned)
        {
            Invoke("SpawnBoss", 10f);
            bossSpawned = true;
        }
    }
}
