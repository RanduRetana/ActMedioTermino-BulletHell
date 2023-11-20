using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Shoot : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform enemyBulletSpawnPoint;
    public float fireRate = 5.0f;
    public int numberOfBullets = 1;
    public float bulletSpeed = 10f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            FireBullets();
        }
    }
    void FireBullets()
    {
        fireRate = 0.3f;
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = Instantiate(EnemyBullet, enemyBulletSpawnPoint.position, enemyBulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(enemyBulletSpawnPoint.forward * bulletSpeed);
        }
    }
}
