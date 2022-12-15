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
    [SerializeField] private float startTimeBetweenShots;
    [SerializeField] private GameObject projectile;
    [SerializeField] private EnemyMovement enemyMovement;

    private float timeBetweenShots;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    private void FixedUpdate()
    {
        EnemyRotation();
    }

    private void EnemyRotation()
    {
        if (GameObject.FindGameObjectWithTag("Player") == true)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                enemyMovement.MoveTowardsTarget(player.position);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                enemyMovement.MoveAway(player.position);
            }



            if (timeBetweenShots <= 0)
            {
                Instantiate(projectile, transform.position, transform.rotation);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }
}

