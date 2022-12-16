using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Random = UnityEngine.Random;
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
    [SerializeField] private GameObject lightningLight;

    [Header("Health")] 
    [SerializeField] public int health, maxHealth = 10;
    [SerializeField] private GameObject parent;
    [SerializeField] private CircleCollider2D playerCollider;

    [Header("Audio")] 
    [SerializeField] private AudioSource fireSoundsAudioSource;
    [SerializeField] private AudioClip[] fireSoundsArray;
    [SerializeField] private AudioSource takeDamageSoundsAudioSource;
    [SerializeField] private AudioClip[] takeDamageSoundsArray;
    [SerializeField] private AudioSource lightningSound;

    [SerializeField] private GameObject earlyGameMusic;
    [SerializeField] private GameObject midGameMusic;
    [SerializeField] private GameObject lateGameMusic;
    [SerializeField] private GameObject gameOverMusic;

    [Header("Gamepad")] 
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotationSmoothing = 1000f;
    [SerializeField] private bool isGamepad;
    
    
    
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private Vector2 movement;
    private Vector2 mousePosition;
    private Vector2 look;
    private Vector2 controllerPosition;
    private InputAction move;
    private InputAction fire;
    private SoundManager soundManager;
    private VolumeSettings volumeSettings;
    private GameManager gameManager;
    private Gamepad gamepad;

    #endregion
    
    private void Awake()
    {
        soundManager = SoundManager.FindObjectOfType<SoundManager>();
        volumeSettings = VolumeSettings.FindObjectOfType<VolumeSettings>();
        gameManager = GameManager.FindObjectOfType<GameManager>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
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

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad");
    }
    private void Update()
    {
        movement = move.ReadValue<Vector2>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        animator.SetFloat("speedx", Mathf.Abs(movement.x));
        animator.SetFloat("speedy", Mathf.Abs(movement.y));
        
        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");
        
    }

    private void FixedUpdate()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + movement * (movementSpeed * Time.fixedDeltaTime));

        if (!isGamepad)
        {
            Vector2 lookDirection = mousePosition - playerRigidbody2D.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            playerRigidbody2D.rotation = angle;
        }
        HandleRotation();
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            if (Mathf.Abs(look.x) > controllerDeadzone || Mathf.Abs(look.y) > controllerDeadzone)
            {
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 180f;
                playerRigidbody2D.rotation = angle;
            }
            


            /*if (Mathf.Abs(look.x) > controllerDeadzone || Mathf.Abs(look.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * look.x + Vector3.forward * look.y;
                if (playerDirection.sqrMagnitude > 0.1f)
                {
                    Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.forward);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newrotation, gamepadRotationSmoothing * Time.deltaTime);
                }
            }*/
        }
    }

    #endregion

    #region Abillities

    private void Fire(InputAction.CallbackContext context)
    {
        animator.SetBool("shot", true);
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootingPoint.up * bulletSpeed, ForceMode2D.Impulse);
        fireSoundsAudioSource.clip=fireSoundsArray[Random.Range(0,fireSoundsArray.Length)];
        fireSoundsAudioSource.PlayOneShot(fireSoundsAudioSource.clip);
        StartCoroutine(SetFalse());

    }
    
    public void Lightning()
    {
        animator.SetBool("lightning", true);
        lightningTrigger.SetActive(true);
        lightningLight.SetActive(true);
        lightningSound.Play();
        StartCoroutine(SetFalseLightning());
    }

    #endregion

    #region Coroutines

    IEnumerator SetFalseLightning()
    {
        yield return new WaitForSeconds(0.1f);
        lightningTrigger.SetActive(false);
        
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("lightning", false);
        lightningLight.SetActive(false);
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


    #endregion

    #region Health

    public void PlayerTakeDamage(int damage)
    {
        health -= damage;
        takeDamageSoundsAudioSource.clip=takeDamageSoundsArray[Random.Range(0,takeDamageSoundsArray.Length)];
        takeDamageSoundsAudioSource.PlayOneShot(takeDamageSoundsAudioSource.clip);
        playerCollider.enabled = !playerCollider.enabled;
        if (health <= 0)
        {
            soundManager.WitchDying();
            DisableInput();
            Destroy(parent);
            Debug.Log("Destroyed");
            earlyGameMusic.SetActive(false);
            midGameMusic.SetActive(false);
            lateGameMusic.SetActive(false);
            gameOverMusic.SetActive(true);
            volumeSettings.Death();
            gameManager.LoseScreen();
            
        }

        StartCoroutine(PlayerCollider());
    }

    #endregion
}
