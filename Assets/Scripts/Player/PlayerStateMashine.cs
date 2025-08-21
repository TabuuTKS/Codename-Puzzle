using UnityEngine;
public class PlayerStateMashine : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 10)] public float WalkSpeed = 2f;
    [Range(0, 10)] public float MoveSpeed = 2f;
    [Range(0, 10)] public float RunSpeed = 4f;

    [Header("Input Keys")]
    [SerializeField] KeyCode RunButton;
    [SerializeField] KeyCode PounceButton;
    [SerializeField] KeyCode WalkButton;

    [Header("Animation")]
    public Animator animator;

    [Header("Colliders")]
    [SerializeField] GameObject Collider;

    [Header("Attacks")]
    public float PounceForce = 1000f;

    //States
    private PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState(); 
    public PlayerRunState runState = new PlayerRunState();
    public PlayerPounceState pounceState = new PlayerPounceState();
    public PlayerWalkState walkState = new PlayerWalkState();

    //Movement Values
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float speed;
    private const float SpeedMultiplyer = 100f;

    //Direction Animaton
    private Vector3 Horizontal = new Vector3(0, 0, 90);

    //Attacks
    [HideInInspector] public bool hadPounce;

    [HideInInspector] public new Rigidbody2D rigidbody2D;
    private Coroutine RunCoroutine;
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        Inputs();
        AnimationDirection();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);

        if (hadPounce)
        {
            SwitchState(pounceState);
            hadPounce = false;
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void Inputs()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(RunButton) && PlayerPrefs.Stamina > 0) { SwitchState(runState); }
            else if (Input.GetKey(WalkButton)) { SwitchState(walkState); }
            else { SwitchState(moveState); PlayerPrefs.isHidden = false; }
        }
        else { SwitchState(idleState); }

        //RunCoroutine Handle
        if (Input.GetKeyDown(RunButton)) { StopRunCoroutine(); }
        else if (Input.GetKeyUp(RunButton)) { StartRunCoroutine(); }

        //Attacks
        if (Input.GetKeyDown(PounceButton))
        {
            Debug.Log("Pounce State");
            hadPounce = true;
            SwitchState(pounceState);
        }
    }


    #region Run Coroutine Helper Methods
    private void StopRunCoroutine()
    {
        if (RunCoroutine != null)
        {
            StopCoroutine(RunCoroutine);
            RunCoroutine = null;
        }
    }

    private void StartRunCoroutine()
    {
        RunCoroutine = StartCoroutine(GameUI.instance.ResetStamina());
    }
    #endregion

    private void AnimationDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == -1) { animator.SetInteger("dir", 1); Collider.transform.localRotation = Quaternion.Euler(Horizontal); }
        else if (Input.GetAxisRaw("Horizontal") == 1) { animator.SetInteger("dir", 3); Collider.transform.localRotation = Quaternion.Euler(Horizontal); }
        else if (Input.GetAxisRaw("Vertical") == 1) { animator.SetInteger("dir", 2); Collider.transform.localRotation = Quaternion.Euler(Vector3.zero); }
        else { animator.SetInteger("dir", 0); Collider.transform.localRotation = Quaternion.Euler(Vector3.zero); }
    }
}
