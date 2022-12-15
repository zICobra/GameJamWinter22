using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Inspector

    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;

    private float damaged;
    [SerializeField] private EnemyMovement enemyMovement;
    

    #endregion

    #region UnityFunktions

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerInputs pI))
        {
            Attack();
            pI.PlayerTakeDamage(damage);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Damage();
        health -= damageAmount;

        if (health <= 0)
        {
            Death();
            OnEnemyKilled?.Invoke(this);
        }
    }

    #endregion



    #region Coroutines
    
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        if (gameObject.TryGetComponent(out PlayerInputs pI))
        {
            Attack();
            pI.PlayerTakeDamage(damage);
        }
    }

    IEnumerator DamageAnimationCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Damage", false);
    }
    IEnumerator AttackAnimationCooldown()
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("Attack", false);
    }

    #endregion

    #region Animations

    private void Damage()
    {
        Debug.Log("Damage");
        animator.SetBool("Damage", true);
        StartCoroutine(DamageAnimationCooldown());
    }


    private void Death()
    {
        enemyMovement.StopMovement();
        animator.SetBool("Death",true);
        Destroy(gameObject, 0.3f);
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        StartCoroutine(AttackAnimationCooldown());
    }

    #endregion
}
