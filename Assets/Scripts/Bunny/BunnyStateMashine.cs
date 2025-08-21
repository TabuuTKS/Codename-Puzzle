using System.Collections;
using UnityEngine;

public class BunnyStateMashine : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1.0f;
    [SerializeField] float RunSpeed = 2.0f;
    [SerializeField] float DetectRegionRadius = 1.0f;
    [SerializeField] LayerMask PlayerLayer;

    //private
    private enum DirectionState { FRONT, BACK, LEFT, RIGHT }
    private enum MovementState { IDLE, MOVE, RUN }

    private DirectionState direction;
    private MovementState movement = MovementState.IDLE;

    private Vector2 directionVector = Vector2.zero;

    private new Rigidbody2D rigidbody;
    private Animator animator;

    private float Speed;

    private RaycastHit2D playerDetected;

    private void Start()
    {
        direction = GetRandomDirectionState();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(StateCycle());
    }

    private void Update()
    {
        playerDetected = Physics2D.CircleCast(transform.position, DetectRegionRadius, Vector2.zero, 0, PlayerLayer);

        switch (direction)
        {
            case DirectionState.LEFT:
                directionVector.Set(-1, 0);
                animator.SetInteger("dir", 2);
                break;
            case DirectionState.RIGHT:
                directionVector.Set(1, 0);
                animator.SetInteger("dir", 3);
                break;
            case DirectionState.FRONT:
                directionVector.Set(0, -1);
                animator.SetInteger("dir", 0);
                break;
            case DirectionState.BACK:
                directionVector.Set(0, 1);
                animator.SetInteger("dir", 1);
                break;
        }

        switch (movement)
        {
            case MovementState.IDLE:
                directionVector = Vector2.zero;
                Speed = 0;
                break;
            case MovementState.MOVE:
                Speed = MoveSpeed;
                break;
            case MovementState.RUN:
                Speed = RunSpeed;
                break;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = directionVector * Speed * 100f * Time.fixedDeltaTime;
    }

    private DirectionState GetRandomDirectionState()
    {
        DirectionState[] values = (DirectionState[])System.Enum.GetValues(typeof(DirectionState));
        int RandomIndex = Random.Range(0, values.Length);
        return values[RandomIndex];
    }

    private IEnumerator StateCycle()
    {
        while (true)
        {
            
            direction = GetRandomDirectionState();
            movement = MovementState.MOVE;
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            if (playerDetected && !PlayerPrefs.isHidden) { movement = MovementState.RUN; yield return new WaitForSeconds(Random.Range(2f, 6f)); }
            if (movement == MovementState.RUN && !playerDetected) { movement = MovementState.IDLE; }

            movement = MovementState.IDLE;
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DetectRegionRadius);
    }
}
