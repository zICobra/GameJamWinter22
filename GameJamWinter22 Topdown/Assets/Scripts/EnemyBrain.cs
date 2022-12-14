using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : ScriptableObject
{
    public abstract void Think(EnemyThinker thinker);
}
