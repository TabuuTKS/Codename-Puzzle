using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(0, 10)] float MoveSpeed = 2f;

    //Movement private
    private Vector2 direction = Vector2.zero;
    private new Rigidbody2D rigidbody;
    private const float SpeedMultiplyer = 100f;

    //Interaction
    private IInteractable CurrentInteractable = null;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null)
        {
            CurrentInteractable.Action();
        }
    }
    private void FixedUpdate()
    {
        rigidbody.linearVelocity = direction.normalized * MoveSpeed * SpeedMultiplyer * Time.fixedDeltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            CurrentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable == CurrentInteractable)
        {
            CurrentInteractable = null;
        }
    }
}
