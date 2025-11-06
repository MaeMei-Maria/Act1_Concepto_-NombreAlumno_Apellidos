using UnityEngine;

public class AmmoGrenadeBox : Interactables
{
    [SerializeField] private float ammoBoxAmount = 5f;
    [SerializeField] private AudioClip reloadAmmoClip;

    public override void Interact(GameObject interactor)
    {
        var main = interactor.GetComponentInParent<PlayerMain>();
        if (main != null)
        {
            main.NotifyAmmoGrenadeCollected(ammoBoxAmount);
        }

        AudioManager.Instance.PlaySFX(reloadAmmoClip);
        gameObject.SetActive(false);
    }
}