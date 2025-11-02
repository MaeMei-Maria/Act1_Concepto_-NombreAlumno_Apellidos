using UnityEngine;

public class AmmoBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private int ammoBoxAmount;
    
    public void OnInteractableActivated()
    {
        interactionIcon.SetActive(true);
    }

    public void OnInteractableDeactivated()
    {
        interactionIcon.SetActive(false);
    }

    public void Interact(GameObject interactor)
    {
        gameObject.SetActive(false);
    }
}
