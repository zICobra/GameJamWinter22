using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedBase = 5f;
    [SerializeField] private float speed;
    [SerializeField] public Transform player;
    
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(2f, 4f);
        speed = randomSpeed + speedBase;
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        Debug.Log(direction);
    }
    
    private void FixedUpdate()
    {
       Vector3 lookDirection = player.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
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
