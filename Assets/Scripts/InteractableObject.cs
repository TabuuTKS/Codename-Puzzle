using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public void Action()
    {
        Debug.Log($"Interaction Successful with {this.gameObject}");
    }
}
