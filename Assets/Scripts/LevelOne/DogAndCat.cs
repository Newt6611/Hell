using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneOneEnemyType 
{
    Dog, DarkDog, Cat, DarkCat
}

public enum SceneOneEnemyState
{
    Idle, Walk, Attack
}

public class DogAndCat : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;

    [SerializeField] SceneOneEnemyType type;

    [SerializeField] private SceneOneEnemyState current_state;
    private SceneOneEnemyState last_state;

    private float power;
    private float health;


    [SerializeField] private float speed;
    
    private bool faceRight = false;

    private void Start()
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        current_state = SceneOneEnemyState.Idle;
        last_state = current_state;
        InitValue();
    }

    private void Update() 
    {
        StateMachine();
    }

    private void FixedUpdate()
    {
        /*
        Vector2 vel = rb.velocity;
        vel.x *= speed * Time.fixedDeltaTime;
        rb.velocity = vel;
        */
    }

    private void StateMachine()
    {
        // Bad State Design
        switch(current_state)
        {
            case SceneOneEnemyState.Idle:
                IdleState();
                break;
            case SceneOneEnemyState.Walk:
                WalkState();
                break;
            case SceneOneEnemyState.Attack:
                AttackState();
                break;
        }
    }

    private void IdleState()
    {
        CheckState(SceneOneEnemyState.Idle);
    }

    private void WalkState()
    {
        CheckState(SceneOneEnemyState.Walk);
    }

    private void AttackState()
    {
        CheckState(SceneOneEnemyState.Attack);
    }

    private void CheckState(SceneOneEnemyState state)
    {
        // Check Animation State
        if(last_state != state)
        {
            PlayAnimation(state);
        }
    }

    private void PlayAnimation(SceneOneEnemyState state)
    {
        last_state = current_state;
        ani.CrossFade(GetAnimaionName(current_state), 0.01f);
    }

    private string GetAnimaionName(SceneOneEnemyState state) 
    {
        switch(state)
        {
            case SceneOneEnemyState.Idle:
                return "idle";
            case SceneOneEnemyState.Walk:
                return "walk";
            case SceneOneEnemyState.Attack:
                return "attack";
            default:
                return "idle";
        }
    }

    public void TakeDamage(int d) 
    {
        health -= d;

        if(health <= 0)
            Destroy(gameObject);
    }

    private void InitValue()
    {
        switch(type)
        {
            case SceneOneEnemyType.Dog:
                health = 4;
                power = 8;
                break;
            case SceneOneEnemyType.DarkDog:
                health = 5;
                power = 12;
                break;
            case SceneOneEnemyType.Cat:
                health = 3;
                power = 6;
                break;
            case SceneOneEnemyType.DarkCat:
                health = 4;
                power = 10;
                break;
        }
    }
}
