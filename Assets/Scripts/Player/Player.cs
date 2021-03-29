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
    private static Player _instance;
    public static Player Instance { get { return _instance; } }

    [SerializeField] private bool showGizmos;


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
    public bool IsJumping { get; set; }
    public bool IsAttack { get; set; }
    public bool CanAttack { get; set; }

    [SerializeField] private Transform jumpPos;
    public Transform JumpPosition { get { return jumpPos; } }

    private AniamtionName currentAnimation;

    private float maxHealth;
    private float health;
    public float mana;

    
    [SerializeField] private PhysicsMaterial2D friction;
    [SerializeField] private PhysicsMaterial2D nonFriction;

    // Layer
    [SerializeField] public LayerMask playerLayer { get; private set; }
    [SerializeField] private LayerMask walkableLayer;
    public LayerMask WalkableLayer { get { return walkableLayer; } }

    // Character States
    private IPlayerState state;
    public Dictionary<string, IPlayerState> stateCache;

    // Components
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public TrailRenderer trail { get; set; }
    public GameFeel gameFeel { get; set; }

    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start() 
    {
        // Components
        rb = GetComponentInChildren<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
        trail = GetComponentInChildren<TrailRenderer>();
        gameFeel = GetComponent<GameFeel>();

        // Init Values
        faceRight = true;
        CanAttack = true;

        health = 100;
        mana = 100;

        playerLayer = LayerMask.GetMask("Player");

        // Init StateCache
        stateCache = new Dictionary<string, IPlayerState>()
        {
            ["idle"] = new IdleState(this, "Player Idle"),
            ["walk"] = new WalkState(this, "Player Walk"),
            ["run"] = new RunState(this, "Player Run"),
            ["jump"] = new JumpState(this, "Player Jump"),
            ["attack"] = new AttackState(this, "Player Attack")
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
        if(GroundDetection())
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

    public bool GroundDetection()
    {
        if(Physics2D.OverlapCircle(JumpPosition.position, 0.5f, WalkableLayer))
            return true;
        else
            return false;
    }

    public void EndJumpState()
    {
        // Decide Which State Should Go When End Jump
        if(IsRun)
            SetState(GetStateCache()["run"]);
        else if(MovementX != 0)
            SetState(GetStateCache()["walk"]);
        else
            SetState(GetStateCache()["idle"]);
    }

    public void TakeDamage(int d)
    {
        //health -= d;
        Debug.Log("Player Damaged");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!IsAttack)
            EndJumpState();
    }
    //////////////////////////////////////////////////





    // Setter And Getter
    public void SetState(IPlayerState _state) 
    {
        this.state.OnExit();
        this.state = _state;
        this.state.OnEntry();       
    }
    
    public void SetTransform(Vector2 position)
    {
        transform.position = position;
    }

    public void PlayAnimation(AniamtionName name)
    {
        if(currentAnimation == name)
            return;

        currentAnimation = name;
        ani.CrossFade(GetAnimaionName(name), 0.01f);
    }

    public void SetSpeed(float s) => this.speed = s;

    public Dictionary<string, IPlayerState> GetStateCache() => stateCache;

    public void SetPhysicsFriction(bool f)
    {
        if(f)
            rb.sharedMaterial = friction;
        else
            rb.sharedMaterial = nonFriction;
    }

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

    private void OnDrawGizmos()
    {
        if(showGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(JumpPosition.position, 0.5f);
        }
    }

}
