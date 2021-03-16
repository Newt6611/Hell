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
    }

    public void EndAttack()
    {
        player.SetState(player.GetStateCache()["idle"]);
    }

    public void Jump()
    {
        player.rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
    }

    public void EndJump()
    {
        player.GroundDetection();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
}
