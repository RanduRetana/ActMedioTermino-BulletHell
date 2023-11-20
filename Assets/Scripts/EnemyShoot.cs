using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform enemyBulletSpawnPoint;
    public float fireRate = 1f;
    public int numberOfBullets = 10;
    public float bulletSpeed = 100f;

    private float timer;
    public delegate void ShootingPattern();
    public ShootingPattern[] shootingPatterns;
    private int currentPattern = 0;
    private float patternChangeTimer = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Inicializar los patrones de disparo
        shootingPatterns = new ShootingPattern[3];
        shootingPatterns[0] = ShootingPattern1;
        shootingPatterns[1] = ShootingPattern2;
        shootingPatterns[2] = ShootingPattern3;
    }

    void Update()
    {
        patternChangeTimer += Time.deltaTime;
        if (patternChangeTimer >= 15f)
        {
            patternChangeTimer = 0f;
            currentPattern = (currentPattern + 1) % shootingPatterns.Length;
        }

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            
            // Seleccionar el patrón de disparo basado en el temporizador
            ShootingPattern shootingPattern = shootingPatterns[currentPattern];
            shootingPattern();
        }
    }

    void ShootingPattern1()
    {
        // Implementa el patrón de disparo 1
        FireBullets();
    }

    void ShootingPattern2()
    {
        // Implementa el patrón de disparo 2
        FireSpiralBullets();
    }

    void ShootingPattern3()
    {
        // Implementa el patrón de disparo 3
        FireFanBullets();
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

    void FireCircleBullets()
    {
        numberOfBullets = 30;
        fireRate = 1f;
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * 360f / numberOfBullets;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            GameObject bullet = Instantiate(EnemyBullet, enemyBulletSpawnPoint.position, rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }
    }

    void FireSpiralBullets()
    {
        numberOfBullets = 20;
        fireRate = 1f;
        bulletSpeed = 50f;
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i <= numberOfBullets; i++)
        {
            Vector3 directionToPlayer = player.position - enemyBulletSpawnPoint.position;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            Vector3 bulletDirection = rotation * directionToPlayer;
            
            GameObject bullet = Instantiate(EnemyBullet, enemyBulletSpawnPoint.position, Quaternion.LookRotation(bulletDirection));
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection.normalized * bulletSpeed;
            
            angle += angleStep;
        }
    }
    void FireFanBullets()
    {
        bulletSpeed = 50f;
        int fanBullets = 15; // Número de balas en el abanico
        float spreadAngle = 45f; // Ángulo total del abanico
        fireRate = 1.2f; // Puedes ajustar esto según lo que necesites

        float angleStep = spreadAngle / (fanBullets - 1); // Calcula el paso de ángulo entre cada bala
        float startingAngle = -spreadAngle / 2; // Ángulo de inicio

        for (int i = 0; i < fanBullets; i++)
        {
            float angle = startingAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0)) * enemyBulletSpawnPoint.rotation;
            GameObject bullet = Instantiate(EnemyBullet, enemyBulletSpawnPoint.position, rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }
    }
}
