using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedBase = 5f;
    [SerializeField] private float speed;
    private GameObject player;
    private Enemy enemy;
    private bool isDead = false;
    
    private Vector2 movement;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(2f, 4f);
        speed = randomSpeed + speedBase;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        RotateToPlayer();
        isDead = enemy.isMurdered;
    }

    void RotateToPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") == true) ;
        {
            if (isDead == false)
            {
                Vector2 lookDirection = transform.position - player.transform.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg +90f;
                rb.rotation = angle;
            }
        }
    }

    public void StopMovement()
    {
        speed = 0;
    }

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
    }
    
    public void MoveAway(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, -speed * Time.deltaTime);
    }



}
