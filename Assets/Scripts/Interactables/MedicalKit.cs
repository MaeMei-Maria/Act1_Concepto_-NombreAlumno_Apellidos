using UnityEngine;

public class MedicalKit : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private int healthAmount;

    public void OnInteractableActivated()
    {
        interactionIcon.SetActive(true);
    }

    public void OnInteractableDeactivated()
    {
        interactionIcon.SetActive(false);
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }
}