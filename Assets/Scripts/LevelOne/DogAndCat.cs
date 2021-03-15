using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneOneEnemyType 
{
    Dog, DarkDog, Cat, DarkCat
}

public class DogAndCat : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;

    [SerializeField] SceneOneEnemyType type;

    private float power;
    private float health;



    [SerializeField] private float speed;
    
    
    private bool faceRight = false;

    void Start() 
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        InitValue();
    }

    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        vel.x *= speed * Time.fixedDeltaTime;
        rb.velocity = vel;
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
