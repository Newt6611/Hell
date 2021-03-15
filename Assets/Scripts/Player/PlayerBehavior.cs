using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Player player;

    // For Attack
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null)
            Debug.Log("Can't Find Player Tag !");
    }

    public void Attack()
    {
        if(!player.CanAttack)
            player.GroundDetection();
        
        // Attack
        Collider2D collider = Physics2D.OverlapCircle(attackPos.position, attackRadius, enemyLayer);
        if(collider)
        {
            Debug.Log("s");
            collider.GetComponent<DogAndCat>().TakeDamage(1);
            player.mana -= 10;
        }

        player.SetState(player.GetStateCache()["idle"]);
    }

    public void Jump()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Vector2.up.y * player.JumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
