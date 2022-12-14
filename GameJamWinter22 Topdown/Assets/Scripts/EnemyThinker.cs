using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    [SerializeField] private EnemyBrain brain;
    void Update()
    {
        brain.Think(this);
    }
}
