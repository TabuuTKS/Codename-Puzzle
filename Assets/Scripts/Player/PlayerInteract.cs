using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] Vector2 InteractBoxSize = Vector2.zero;
    [SerializeField] GameObject PressE;
    [SerializeField] LayerMask IntractLayer;
    //Interaction
    private IInteractable interactableObject;
    Collider2D hitCollider = null;

    private void Update()
    {
        hitCollider = Physics2D.OverlapBox(
            transform.position,
            InteractBoxSize,
            0f,
            IntractLayer
        );

        if (hitCollider != null && hitCollider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactableObject = interactable;
            PressE.SetActive(true);
        }
        else
        {
            interactableObject = null;
            PressE.SetActive(false);
        }

        if (interactableObject != null && Input.GetKeyDown(KeyCode.E))
        {
            interactableObject.Action();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, (Vector3)InteractBoxSize);
    }
}
