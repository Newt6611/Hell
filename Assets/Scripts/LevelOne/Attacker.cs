using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SceneOneType
{
    cat, dog
}

public class Attacker : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    
    [SerializeField] private bool showGizmos;
    [SerializeField] private SceneOneType type;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRadius;

    public void Attack()
    {
        switch(type)
        {
            case SceneOneType.cat:
                SceneOneCat cat = parent.GetComponent<SceneOneCat>();
                if(Physics2D.OverlapCircle(attackPos.position, attackRadius, playerLayer))
                {
                    Player.Instance.TakeDamage(cat.power);
                }
                break;
        }
    }

    public void BackToIdle()
    {
        switch(type)
        {
            case SceneOneType.cat:
                SceneOneCat cat = parent.GetComponent<SceneOneCat>();
                cat.BackToIdle();
                break;
        }
    }

    private void OnDrawGizmos() 
    {
        if(showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRadius);
        }
    }

}
