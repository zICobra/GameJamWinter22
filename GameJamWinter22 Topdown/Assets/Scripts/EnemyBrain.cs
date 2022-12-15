using UnityEngine;

public abstract class EnemyBrain : ScriptableObject
{
    public abstract void Think(EnemyThinker thinker);
}
