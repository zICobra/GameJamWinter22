using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    [SerializeField] private EnemyBrain brain;
    void Update()
    {
        brain.Think(this);
    }
}
