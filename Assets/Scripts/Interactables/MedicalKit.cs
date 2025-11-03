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
        PlayerHealthSystem playerHealthSystem = interactor.GetComponentInParent<PlayerHealthSystem>();
        
        if (playerHealthSystem != null)
        {
            Debug.Log("Interact with PlayerHealthSystem");   
            playerHealthSystem.Heal(healthAmount);
        }
        
        gameObject.SetActive(false);
    }
}