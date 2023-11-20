using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 50f;
    private Vector3 startPosition;
    public GameManager gameManager;
    //public int bulletCountInt = 0;

   void Start()
    {
        startPosition = transform.position;
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        GameManager.bulletCount += 1;
    }


    void Update()
    {
        // Mover la bala hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;

        // Comprobar si la bala ha alcanzado la distancia máxima
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
        //print bullet count to debug
        //Console.WriteLine(GameManager.bulletCount);
        //TextMeshPro bulletCountText = GameObject.Find("BulletCount").GetComponent<TextMeshPro>();
        //bulletCountText.text = "Bullets: " + GameManager.bulletCount.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Comprobar si la bala ha colisionado con el enemigo
        if (collision.gameObject.CompareTag("Boss"))
        {
            // Llamar al método TakeDamage en el enemigo
            collision.gameObject.GetComponent<EnemyMovement>().TakeDamage(1);
        }

        // Destruir la bala independientemente de lo que colisionó con
        Destroy(gameObject, 0.1f);
    }
    void onCollisionWithBullet(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            //GameManager.bulletCount -= 1;
        }
    }
    void OnDestroy()
    {
        GameManager.bulletCount -= 1;
    }
}
