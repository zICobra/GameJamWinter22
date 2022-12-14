using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlayerInputs : MonoBehaviour
{
    #region Inspector

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    //[SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    
    [Header("Abillities")] 
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    //[SerializeField] private GameObject lightningPrefab;
    [SerializeField] private GameObject lightningTrigger;
    [SerializeField] private float lightningCooldown;

    [Header("Health")] 
    [SerializeField] private int health, maxHealth = 10;
    [SerializeField] private GameObject parent;
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Vector2 mousePosition;
    private InputAction move;
    private InputAction fire;
    private InputAction lightning;
    private float usedLightning;

    #endregion
    
    private void Awake()
    {
        playerControls = new PlayerControls();
        health = maxHealth;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        
        lightning = playerControls.Player.Lightning;
        lightning.Enable();
        lightning.performed += Lightning;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }
    
    public void EnableInput()
    {
        playerControls.Enable();
    }

    public void DisableInput()
    {
        playerControls.Disable();
    }

    #region Movement
    private void Update()
    {
        movement = move.ReadValue<Vector2>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        animator.SetFloat("speedx", Mathf.Abs(movement.x));
        animator.SetFloat("speedy", Mathf.Abs(movement.y));
    }

    private void FixedUpdate()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + movement * (movementSpeed * Time.fixedDeltaTime));
        
        Vector2 lookDirection = mousePosition - playerRigidbody2D.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        playerRigidbody2D.rotation = angle;
        //RotateSprite();
    }

    /*private void RotateSprite()
    {
        if (movement != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, movement);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            playerRigidbody2D.MoveRotation(rotation);
        }
    }*/
    
    #endregion

    #region Abillities

    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("shot", false);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        animator.SetBool("shot", true);
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootingPoint.up * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(SetFalse());

    }

    IEnumerator SetFalseLightning()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("lightning", false);
        lightningTrigger.SetActive(false);
    }
    private void Lightning(InputAction.CallbackContext context)
    {
        if (Time.time - usedLightning < lightningCooldown)
        {
            return;
        }
    
        usedLightning = Time.time;
        animator.SetBool("lightning", true);
        lightningTrigger.SetActive(true);
        StartCoroutine(SetFalseLightning());

    }

    #endregion

    #region Health

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(parent);
        }
    }

    #endregion
}
