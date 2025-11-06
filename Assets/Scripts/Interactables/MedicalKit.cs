using UnityEngine;

public class MedicalKit : Interactables
{
    [SerializeField] private float healAmount = 15f;

    public override void Interact(GameObject interactor)
    {
        var main = interactor.GetComponentInParent<PlayerMain>();
        if (main != null)
        {
            main.NotifyHealed(healAmount);
        }

        gameObject.SetActive(false);
    }
}