using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(5);
        }
    }
}
