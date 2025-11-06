using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private GameObject hudInstance;
    private GameObject dialogueInstance;
    private DialogueSystem dialogueSystem;

    private void Awake()
    {
        dialogueInstance = GetComponentInChildren<DialogueSystem>().gameObject;
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        hudInstance.SetActive(false);
        dialogueInstance.SetActive(false);

        // Estado inicial: mostrar HUD, ocultar diálogo
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if (hudInstance != null)
            hudInstance.SetActive(false);

        if (dialogueInstance != null)
        {
            dialogueInstance.SetActive(true);
            dialogueSystem?.StartDialogue(); // Inicia el diálogo manualmente
        }
    }

    public void ShowHUD()
    {
        if (hudInstance != null)
            hudInstance.SetActive(true);

        if (dialogueInstance != null)
            dialogueInstance.SetActive(false);
    }

    // Este método será llamado por el DialogueSystem al terminar el diálogo
    public void OnDialogueEnded()
    {
        ShowHUD();
    }
}