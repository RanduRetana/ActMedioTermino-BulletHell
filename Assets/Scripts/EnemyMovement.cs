using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float amplitude = 5f;

    private Vector3 startPosition;
    //public Transform player;

    public float minDistance = 5f;
    public float maxDistance = 20f;
    public float minHeight = 0.4f;
    public float maxHeight = 5f;
    public float followSpeed = 10f;
    public float rotationSpeed = 10f;
    public float Health = 100f;
    public int lives = 3;
    private Transform player;
    private GameManager gameManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcular la dirección hacia el jugador
        Vector3 directionToPlayer = player.position - transform.position;

        // Rotar al enemigo para que mire hacia el jugador
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToPlayer, Time.deltaTime * rotationSpeed);

        // Mover al enemigo hacia el jugador si está más allá de la distancia mínima
        if (directionToPlayer.magnitude > minDistance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        // Cambiar la altura del enemigo basado en la distancia al jugador
        //float distanceRatio = Mathf.Clamp01((directionToPlayer.magnitude - minDistance) / (maxDistance - minDistance));
        //float height = Mathf.Lerp(minHeight, maxHeight, distanceRatio);
        //transform.position = new Vector3(transform.position.y, height, transform.position.z);

        //movingVertical();
        //movingHorizontal();
        //movingCircular();
        moveEnemy();
    }
    public void TakeDamage(int damage)
    {
        lives -= damage;

        if (lives <= 0)
        {
            Destroy(gameObject);
            gameManager.EnemyDestroyed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    void movingHorizontal()
    {
        transform.position = startPosition + amplitude * new Vector3(Mathf.Sin(Time.time * speed), 0f, 0f);
    }
    void movingVertical()
    {
    // Define las alturas en las que el enemigo puede moverse
    float[] heights = { 2.0f, 5.0f, 7.0f, 11.0f };

    // Calcula la nueva posición en el eje Y sin pasar por debajo del piso
    float newY = heights[Mathf.RoundToInt(Mathf.PingPong(Time.time * speed, heights.Length - 1))];

    // Asegúrate de que la altura del enemigo nunca supere el suelo
    newY = Mathf.Clamp(newY, 3.48f, maxHeight);

    // Interpola suavemente hacia la nueva posición en el eje Y
    float targetY = newY;
    float currentY = transform.position.y;
    float smoothTime = 0.1f; // Puedes ajustar este valor para cambiar la velocidad de la interpolación
    float smoothY = Mathf.SmoothDamp(currentY, targetY, ref smoothTime, smoothTime);

    // Actualiza la posición del enemigo
    transform.position = new Vector3(transform.position.x, smoothY, transform.position.z);
    }


    void movingCircular()
    {
        transform.position = startPosition + amplitude * new Vector3(Mathf.Sin(Time.time * speed), 0f, Mathf.Cos(Time.time * speed));
    }

    void moveEnemy()
    {
        // Ajustes para el movimiento
        float horizontalSpeed = speed; // Velocidad de movimiento horizontal
        float verticalSpeed = speed;   // Velocidad de movimiento vertical
        float time = Time.time * speed;

        // Calcula la posición horizontal
        float horizontalMovement = amplitude * Mathf.Sin(time);

        // Calcula la posición vertical
        float[] heights = { 2.0f, 5.0f, 7.0f, 11.0f };
        float newY = heights[Mathf.RoundToInt(Mathf.PingPong(time, heights.Length - 1))];
        newY = Mathf.Clamp(newY, 3.48f, maxHeight);
        float smoothTime = 0.1f;
        float smoothY = Mathf.SmoothDamp(transform.position.y, newY, ref smoothTime, smoothTime);

        // Actualiza la posición del enemigo
        transform.position = new Vector3(startPosition.x + horizontalMovement, smoothY, transform.position.z);
    }
}
