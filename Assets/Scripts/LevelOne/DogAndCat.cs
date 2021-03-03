using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animation Problem

public class DogAndCat : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    
    bool faceRight = false;

    void Start() 
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {

    }

    void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        vel.x *= speed * Time.fixedDeltaTime;
        rb.velocity = vel;
    }
}
