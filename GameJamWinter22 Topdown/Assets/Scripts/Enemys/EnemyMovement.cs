using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedBase = 5f;
    [SerializeField] private float speed;
    

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        float randomSpeed = Random.Range(2f, 4f);
        speed = randomSpeed + speedBase;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }



}
