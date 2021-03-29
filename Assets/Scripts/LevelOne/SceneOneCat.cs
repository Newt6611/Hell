using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum CatBehavior 
{
    idle, run, clean, attack, empty    
}

public class SceneOneCat : MonoBehaviour, Enemy
{
    [SerializeField] private float speed;
    public int power;
    [SerializeField] private int health;

    private bool faceRight = true;

    private CatBehavior current_state;
    private CatBehavior last_state;

    // For Change State
    private float timer = 5;

    // Attack
    private bool canAttack = false;
    private float attackTimer = 2.0f;
    private float attackTimerBTW;

    [Header("Radius")]
    [SerializeField] private bool showGizmos;
    [SerializeField] private float findPlayerRaidius;
    [SerializeField] private float attackRadius;

    // Component
    private Animator ani;
    private Rigidbody2D rb;

    private void Start() 
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        current_state = CatBehavior.clean;
        last_state = CatBehavior.empty;

    }

    private void Update()
    {
        switch(current_state)
        {
            case CatBehavior.idle:
                Idle();
                break;
            case CatBehavior.run:
                Run();
                break;
            case CatBehavior.clean:
                Clean();
                break;
            case CatBehavior.attack:
                Attack();
                break;
        }

        if(attackTimerBTW <= 0)
        {
            canAttack = true;
            attackTimerBTW = attackTimer;
        }
        else
            attackTimerBTW -= Time.deltaTime;

    }

    private void Idle()
    {
        // For PlayAnimation
        if(CheckState(CatBehavior.idle))
        {
            timer = Random.Range(4, 10);
            // Random 2 differnt idle animation
            int index = Random.Range(0, 2);
            if(index == 0)
                PlayAnimation("idle");
            else if(index == 1)
                PlayAnimation("idle2");
        }

        // Do Behavior
        if(Physics2D.OverlapCircle(transform.position, attackRadius, Player.Instance.playerLayer))
        {
            if(canAttack)
                current_state = CatBehavior.attack;
        }
        else if(Physics2D.OverlapCircle(transform.position, findPlayerRaidius, Player.Instance.playerLayer))
        {
            current_state = CatBehavior.run;
        }

        if(timer <= 0)
        {   
            current_state = CatBehavior.clean;
        }
        else
            timer -= Time.deltaTime;
    }

    // Chase Player
    private void Run()
    {
        // For PlayAnimation
        if(CheckState(CatBehavior.run))
        {
            PlayAnimation("run");
        }

        // Do Behavior
        if(Physics2D.OverlapCircle(transform.position, attackRadius, Player.Instance.playerLayer))
        {
            if(canAttack)
                current_state = CatBehavior.attack;
        }
        else if(!Physics2D.OverlapCircle(transform.position, findPlayerRaidius, Player.Instance.playerLayer))
        {
            current_state = CatBehavior.idle;
        }

        // Chase
        if(Vector2.Distance(transform.position, Player.Instance.transform.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, speed * Time.deltaTime);
        }
        
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        if(direction.x > 0 && !faceRight)
            Flip();
        else if(direction.x < 0 && faceRight)
            Flip();
    }

    private void Clean()
    {
        // For PlayAnimation
        if(CheckState(CatBehavior.clean))
        {
            timer = Random.Range(3, 10);
            PlayAnimation("clean");   
        }

        if(Physics2D.OverlapCircle(transform.position, findPlayerRaidius, Player.Instance.playerLayer))
        {
            current_state = CatBehavior.run;
        }

        // Do Behavior
        if(timer <= 0)
        {
            current_state = CatBehavior.idle;
        }
        else
            timer -= Time.deltaTime;

    }

    private void Attack()
    {
        canAttack = false;
        // For PlayAnimation
        if(CheckState(CatBehavior.attack))
        {
            PlayAnimation("attack");   
        }

        // Flip
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        if(direction.x > 0 && !faceRight)
            Flip();
        else if(direction.x < 0 && faceRight)
            Flip();
    }

    private bool CheckState(CatBehavior state)
    {
        if(last_state == state)
            return false;

        last_state = current_state;
        return true;
    }

    private void PlayAnimation(string animationName) 
    {
        ani.CrossFade(animationName, 0.1f);
    }

    public void TakeDamage(int d) 
    {
        health -= d;
        Debug.Log("Cat Damaged");
        /*
        if(health <= 0)
        {
            Debug.Log("Cat Dead");
            Destroy(gameObject);
        }*/
    }

    public void BackOff() 
    {
        float xdir = transform.position.x - Player.Instance.transform.position.x;
        xdir = xdir > 0 ? 1 : -1;
        rb.AddForce(Vector2.right * xdir * 40, ForceMode2D.Impulse);
    }


    public void BackToIdle()
    {
        current_state = CatBehavior.idle;
    }

    private void Flip()
    {
        faceRight = !faceRight;
        if(!faceRight)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if(showGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, findPlayerRaidius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
