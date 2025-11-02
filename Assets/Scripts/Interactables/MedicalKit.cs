using UnityEngine;

public class MedicalKit : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private float healthAmount = 15f;

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
        if (interactor.GetComponentInParent<PlayerHealthSystem>())
        {
            Debug.Log("Interact with PlayerHealthSystem");   
        }
        
        gameObject.SetActive(false);
    }
}