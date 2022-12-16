using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    #region Inspector

    public static event Action<Enemy> OnEnemyKilled;
    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private bool hasArmor;
    [SerializeField] private AudioSource strikeSoundAudioSource;
    [SerializeField] private AudioClip[] strikeSoundArray;
    [SerializeField] private AudioSource attackVocalAudioSource;
    [SerializeField] private AudioClip[] attackVocalArray;
    [SerializeField] private Collider2D collider;

    private float damaged;
    [SerializeField] private EnemyMovement enemyMovement;
    
    public SoundManager soundManager;
    public bool isMurdered = false;
    
    

    #endregion

    #region UnityFunktions

    private void Start()
    {
        soundManager = SoundManager.FindObjectOfType<SoundManager>();
        collider = GetComponent<Collider2D>();
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerInputs pI))
        {
            Attack();
            strikeSoundAudioSource.clip=strikeSoundArray[Random.Range(0,strikeSoundArray.Length)];
            strikeSoundAudioSource.PlayOneShot(strikeSoundAudioSource.clip);
            attackVocalAudioSource.clip=attackVocalArray[Random.Range(0,attackVocalArray.Length)];
            attackVocalAudioSource.PlayOneShot(attackVocalAudioSource.clip);
            pI.PlayerTakeDamage(damage);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Damage();
        health -= damageAmount;
        if (hasArmor == true)
        {
           soundManager.KnightGetHit();
           soundManager.BulletImpactArmor();
        }

        else
        {
            soundManager.PeasantGetHit();
            soundManager.BulletImpactNoArmor();
        }

        if (health <= 0)
        {
            collider.enabled = !collider.enabled;
            Death();
            OnEnemyKilled?.Invoke(this);
            if (hasArmor == true)
            {
                soundManager.KnightDying();
            }

            else
            {
                soundManager.PeasantDying();
            }
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
        isMurdered = true;
        enemyMovement.StopMovement();
        animator.SetBool("Death",true);
        Destroy(gameObject, 1f);
    }

    private void Attack()
    {
        animator.SetBool("Attack", true);
        StartCoroutine(AttackAnimationCooldown());
    }

    #endregion
}
