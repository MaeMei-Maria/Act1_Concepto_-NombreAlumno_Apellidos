using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected PlayerAmmoSystem playerAmmoSystem;

    public abstract void OnUse();
}
