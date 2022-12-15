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
    [SerializeField] private GameObject lightningTrigger;
    [SerializeField] public float lightningCooldown;

    [Header("Health")] 
    [SerializeField] public int health, maxHealth = 10;
    [SerializeField] private GameObject parent;
    [SerializeField] private CircleCollider2D playerCollider;
    
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Vector2 mousePosition;
    private InputAction move;
    private InputAction fire;

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
        
        
    }

    #endregion

    #region Abillities

    private void Fire(InputAction.CallbackContext context)
    {
        animator.SetBool("shot", true);
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootingPoint.up * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(SetFalse());

    }
    
    public void Lightning()
    {
        animator.SetBool("lightning", true);
        lightningTrigger.SetActive(true);
        StartCoroutine(SetFalseLightning()); 
    }

    #endregion

    #region Coroutines

    IEnumerator SetFalseLightning()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("lightning", false);
        lightningTrigger.SetActive(false);
    }
    
    IEnumerator SetFalse()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("shot", false);
    }

    IEnumerator PlayerCollider()
    {
        yield return new WaitForSeconds(2f);
        playerCollider.enabled = !playerCollider.enabled;
    }

    IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(lightningCooldown);
    }

    #endregion

    #region Health

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        playerCollider.enabled = !playerCollider.enabled;
        if (health <= 0)
        {
            DisableInput();
            Destroy(parent);
            Debug.Log("Destroyed");
        }

        StartCoroutine(PlayerCollider());
    }

    #endregion
}
