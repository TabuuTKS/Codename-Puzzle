using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(0, 10)] float MoveSpeed = 2f;
    [SerializeField] [Range(0, 10)] float RunSpeed = 4f;

    //Movement private
    private float speed;
    private Vector2 direction = Vector2.zero;
    private new Rigidbody2D rigidbody;
    private const float SpeedMultiplyer = 100f;

    //Interaction
    private IInteractable CurrentInteractable = null;

    //Player Run
    [HideInInspector] public bool canRun = true;
    private Coroutine RunCoroutine;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        speed = MoveSpeed;
    }

    private void Update()
    {
        PlayerInput();
        PlayerRun();
    }

    private void PlayerInput()
    {
        direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null)
        {
            CurrentInteractable.Action();
        }
    }

    private void PlayerRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun)
        {
            speed = RunSpeed;
            if (RunCoroutine != null)
            {
                StopCoroutine(RunCoroutine);
                RunCoroutine = null;
            }
        }
        if(Input.GetKey(KeyCode.LeftShift) && canRun && direction != Vector2.zero)
        {
            GameUI.instance.StaminaBar();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = MoveSpeed;
            RunCoroutine = StartCoroutine(GameUI.instance.ResetStamina()); 
        }
        if (PlayerPrefs.Stamina <= 0)
        {
            speed = MoveSpeed;
        }
    }
    private void FixedUpdate()
    {
        rigidbody.linearVelocity = direction.normalized * speed * SpeedMultiplyer * Time.fixedDeltaTime;
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
