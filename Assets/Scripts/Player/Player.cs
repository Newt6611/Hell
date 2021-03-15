using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AniamtionName
{
    idle, walk, run, jump, attack
}

public class Player : MonoBehaviour
{
    // Movement
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get {return walkSpeed; } }
    
    [SerializeField] private float runSpeed;
    public float RunSpeed { get { return runSpeed; } }

    [SerializeField] private float jumpForce;
    public float JumpForce { get { return jumpForce; } }

    private Vector2 movement;
    public float MovementX { get { return movement.x; } }

    private float speed;

    private bool faceRight = true;
    public bool IsRun { get; set; }
    public bool CanJump { get; set; }
    public bool CanAttack { get; set; }

    [SerializeField] private Transform jumpPos;
    public Transform JumpPosition { get { return jumpPos; } }

    private AniamtionName currentAnimation;

    // Layer
    [SerializeField] private LayerMask walkableLayer;
    public LayerMask WalkableLayer { get { return walkableLayer; } }

    // Character States
    private IPlayerState state;
    public Dictionary<string, IPlayerState> stateCache;

    // Components
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    [SerializeField] private InputReader inputReader;

    private void Start() 
    {
        // Components
        rb = GetComponentInChildren<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();

        // Init Values
        faceRight = true;
        CanJump = true;
        CanAttack = true;

        // Init StateCache
        stateCache = new Dictionary<string, IPlayerState>()
        {
            ["idle"] = new IdleState(this),
            ["walk"] = new WalkState(this),
            ["run"] = new RunState(this),
            ["jump"] = new JumpState(this),
            ["attack"] = new AttackState(this)
        };

        // Begin Character State
        state = stateCache["idle"];
        state.OnEntry();
    }

    private void OnEnable()
    {
        // Init Input Callbacks
        inputReader.movementEvent += MovementAction;
        inputReader.attackEvent += AttackAction;
        inputReader.runEvent += RunButtonAction;
        inputReader.jumpEvent += JumpAction;
        inputReader.controlsChangeEvent += OnControlsChanged;
    }

    private void OnDisable()
    {
        inputReader.movementEvent -= MovementAction;
        inputReader.attackEvent -= AttackAction;
        inputReader.runEvent -= RunButtonAction;
        inputReader.jumpEvent -= JumpAction;
        inputReader.controlsChangeEvent -= OnControlsChanged;
    }
    

    private void Update() 
    {
        state.OnUpdate();
    }

    private void FixedUpdate() 
    {
        state.OnFixedUpdate();
    }


    




    // Input Actions Callback //////////////////////////
    private void MovementAction(Vector2 value)
    {
        movement.x = value.x;
    }

    private void JumpAction()
    {
        if(CanJump)
            SetState(stateCache["jump"]);
    }

    private void AttackAction()
    {
        CanAttack = false;
        SetState(stateCache["attack"]);
    }

    private void RunButtonAction() 
    {
        IsRun = !IsRun;
    }

    private void OnControlsChanged()
    {
        // Todo
    }




    /////// Player Behavior ///////////////////////////
    public void Movement() 
    {
        rb.velocity = new Vector2(movement.x * speed * Time.fixedDeltaTime, rb.velocity.y);

        if(movement.x > 0 && !faceRight)
            Flip();
        else if(movement.x < 0 && faceRight)
            Flip();
    }

    private void Flip()
    {
        faceRight = !faceRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void GroundDetection()
    {
        if(Physics2D.OverlapCircle(JumpPosition.position, 0.3f, WalkableLayer) && !CanJump)
        {
            if(MovementX == 0)
                SetState(GetStateCache()["idle"]);
            else if(IsRun)
                SetState(GetStateCache()["run"]);
            else if(!IsRun)
                SetState(GetStateCache()["walk"]);
            CanJump = true;
        }
    }
    //////////////////////////////////////////////////





    // Setter And Getter
    public void SetState(IPlayerState _state) 
    {
        this.state.OnExit();
        this.state = _state;
        this.state.OnEntry();       
    }
    

    public void PlayAnimation(AniamtionName name)
    {
        if(currentAnimation == name)
            return;

        currentAnimation = name;
        ani.Play(GetAnimaionName(name));
    }

    public void SetSpeed(float s) => this.speed = s;

    public Dictionary<string, IPlayerState> GetStateCache() => stateCache;

    private string GetAnimaionName(AniamtionName name) 
    {
        switch(name)
        {
            case AniamtionName.idle:
                return "idle";
            case AniamtionName.walk:
                return "walk";
            case AniamtionName.run:
                return "run";
            case AniamtionName.jump:
                return "jump";
            case AniamtionName.attack:
                return "attack";
            default:
                return "idle";
        }
    }
}
