using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] float stoppingDistance;
    [SerializeField] private float retreatDistance;

    private float timeBetweenShots;
    [SerializeField] private float startTimeBetweenShots;

    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform player;
    
    [SerializeField] private float speedBase = 5f;
    [SerializeField] private float speed;

    [SerializeField] private EnemyMovement enemyMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        float randomSpeed = Random.Range(2f, 4f);
        speed = randomSpeed + speedBase;

        timeBetweenShots = startTimeBetweenShots;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            enemyMovement.MoveTowardsTarget(player.position);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            enemyMovement.MoveAway(player.position);
        }



        if (timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}

