using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PalatinRangeAttack : MonoBehaviour
{
    [SerializeField] float stoppingDistance;
    [SerializeField] private float rangeDistance;
    [SerializeField] private float startTimeBetweenShots;
    [SerializeField] private GameObject projectile;
    [SerializeField] private EnemyMovement enemyMovement;
    

    private float timeBetweenShots;
    private bool inRange;
    
    public SoundManager soundManager;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        soundManager = SoundManager.FindObjectOfType<SoundManager>();
        
        inRange = true;

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
                inRange = true;
                enemyMovement.MoveTowardsTarget(player.position);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > rangeDistance)
            {
                inRange = true;
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < rangeDistance)
            {
                inRange = false;
                enemyMovement.MoveTowardsTarget(player.position);
            }



            if (inRange)
            {
                if (timeBetweenShots <= 0)
                {
                    Instantiate(projectile, transform.position, transform.rotation);
                    soundManager.PaladinAttack();
                    timeBetweenShots = startTimeBetweenShots;
                }
                else
                {
                    timeBetweenShots -= Time.deltaTime;
                }
            }
        }
    }
}

