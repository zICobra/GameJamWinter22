using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName ="Brains/Chase")]
public class ChaseBrain : EnemyBrain
{
    [SerializeField] private string targetTag;
    
    public override void Think(EnemyThinker thinker)
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target)
        {
            var movement = thinker.gameObject.GetComponent<EnemyMovement>();
            if (movement)
            {
                movement.MoveTowardsTarget(target.transform.position);
            }
        }
    }
}
