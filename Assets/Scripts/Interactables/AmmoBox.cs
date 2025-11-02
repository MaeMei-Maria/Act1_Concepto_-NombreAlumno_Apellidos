using UnityEngine;

public class AmmoBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionIcon;
    [SerializeField] private float ammoBoxAmount = 15f;
    
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
        if (interactor.TryGetComponent<PlayerAmmoSystem>(out PlayerAmmoSystem ammoSystem))
        {
            /* Mathf.Approximately compara dos valores float considerando errores de precisión.
               Si la munición actual ya es aproximadamente igual al máximo, salimos del método
               para no sumar más balas de las permitidas. */
            if (Mathf.Approximately(ammoSystem.currentAmmoGun, ammoSystem.MaxAmmoGun)) return;
            
            ammoSystem.GetGunAmmo(ammoBoxAmount);
        }
        
        gameObject.SetActive(false);
    }
}
