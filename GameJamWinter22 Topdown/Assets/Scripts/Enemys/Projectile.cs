using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private bool isPaladinAttack;

    private Transform player;
    private Vector2 target;
    private PlayerInputs playerInputs;
    public SoundManager soundManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInputs = FindObjectOfType<PlayerInputs>();
        soundManager = SoundManager.FindObjectOfType<SoundManager>();

        target = new Vector2(player.position.x, player.position.y);
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        if (col.CompareTag("Player"))
        {
            playerInputs.PlayerTakeDamage(damage);

            if (isPaladinAttack)
            {
                soundManager.PaladinAttack();
            }
            else
            {
                soundManager.WitchHitByArrow();
            }
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void Health(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            DestroyProjectile();
        }
    }
}
