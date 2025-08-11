using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    //Interaction
    private IInteractable CurrentInteractable = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentInteractable != null)
        {
            CurrentInteractable.Action();
        }
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
