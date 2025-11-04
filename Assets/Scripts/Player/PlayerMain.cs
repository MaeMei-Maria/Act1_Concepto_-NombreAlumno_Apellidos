using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    // Evento que notificará que hubo una interacción
    public event Action OnInteraction;

    public void NewInteraction()
    {
        Debug.Log("Nueva interacción detectada, notificando subsistemas...");
        OnInteraction?.Invoke();
    }
}