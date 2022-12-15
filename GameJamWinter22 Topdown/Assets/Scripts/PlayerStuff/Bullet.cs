using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] GameObject hitEffect;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        /*GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);*/
        if (col.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (col.gameObject.TryGetComponent(out Projectile projectile))
        {
            projectile.Health(1);
        }

        if (col.gameObject.TryGetComponent(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
}
