using UnityEngine;

public class AmmoBox : Interactables
{
    [SerializeField] private float ammoBoxAmount = 15f;

    public override void Interact(GameObject interactor)
    {
        var main = interactor.GetComponentInParent<PlayerMain>();
        if (main != null)
        {
            main.NotifyAmmoGunCollected(ammoBoxAmount);
        }

        gameObject.SetActive(false);
    }
}