using UnityEngine;

public class PlayerStateMashine : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 10)] public float MoveSpeed = 2f;
    [Range(0, 10)] public float RunSpeed = 4f;

    [Header("Input Keys")]
    [SerializeField] KeyCode RunButton;

    //States
    private PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerMoveState moveState = new PlayerMoveState(); 
    public PlayerRunState runState = new PlayerRunState();

    //Movement Values
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float speed;
    private const float SpeedMultiplyer = 100f;

    [HideInInspector] public new Rigidbody2D rigidbody2D;
    private Coroutine RunCoroutine;
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentState.UpdateState(this);
        Inputs();
    }

    private void FixedUpdate()
    {
        rigidbody2D.linearVelocity = direction.normalized * speed * SpeedMultiplyer * Time.fixedDeltaTime;
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
            if (Input.GetKey(RunButton) && PlayerPrefs.Stamina > 0)
            {
                SwitchState(runState);
            }
            else
            {
                SwitchState(moveState);
            }
        }
        else
        {
            SwitchState(idleState);
        }

        //RunCoroutine Handle
        if (Input.GetKeyDown(RunButton)) { StopRunCoroutine(); }
        else if (Input.GetKeyUp(RunButton)) { StartRunCoroutine(); }
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
}
