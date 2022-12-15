using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Transform player;
    private Vector2 target;
    private PlayerInputs playerInputs;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInputs = FindObjectOfType<PlayerInputs>();

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
        if (col.CompareTag("Player"))
        {
            playerInputs.PlayerTakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
