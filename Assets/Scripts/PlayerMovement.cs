using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 5.0f;
    private bool groundedPlayer;
    public float gravityValue = -9.81f;
    public float jumpHeight = 3.0f;
    public int lives = 20;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
    }

    public void ProcessMovement(Vector2 movement)
    {
        Vector3 move = Vector3.zero;
        move.x = movement.x;
        move.z = movement.y;
        controller.Move(transform.TransformDirection(move) * Time.deltaTime * playerSpeed);
        if(controller.isGrounded)
            playerVelocity.y = -2.0f;
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }
    public void TakeDamage()
    {
        lives -= 1;
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
