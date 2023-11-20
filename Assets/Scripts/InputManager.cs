using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMovement movement;
    private PlayerLook look;
    public GameObject Bullet;
    public Transform bulletSpawnPoint;
    //private bool hasClicked = false;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        movement = GetComponent<PlayerMovement>();
        onFoot.Jump.performed += ctx => movement.Jump();
        look = GetComponent<PlayerLook>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 1500);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.ProcessMovement(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
