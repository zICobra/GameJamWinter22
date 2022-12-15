using UnityEngine;

public class lightning : MonoBehaviour
{
    
    public SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.FindObjectOfType<SoundManager>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemyComponent))
        {
            soundManager.LightningHit();
            enemyComponent.TakeDamage(5);
        }
    }
}
