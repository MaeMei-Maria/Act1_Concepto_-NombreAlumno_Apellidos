using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueManagerSo _dialogueManagerSo;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;

    private int currentLine = 0;
    private bool isTyping = false;
    private bool isActive = false;

    private void OnEnable()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnContinueDialogue += ContinueDialogue;
    }

    private void OnDisable()
    {
        if (InputController.Instance != null)
            InputController.Instance.OnContinueDialogue -= ContinueDialogue;
    }

    public void StartDialogue()
    {
        currentLine = 0;
        isActive = true;
        StartCoroutine(ShowLine());
    }

    private void ContinueDialogue()
    {
        if (!isActive) return;

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = _dialogueManagerSo.dialogueLines[currentLine].lineText;
            isTyping = false;
        }
        else
        {
            currentLine++;
            if (currentLine < _dialogueManagerSo.dialogueLines.Length)
            {
                StartCoroutine(ShowLine());
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = "";
        speakerNameText.text = _dialogueManagerSo.dialogueLines[currentLine].speakerName;

        foreach (char c in _dialogueManagerSo.dialogueLines[currentLine].lineText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        isActive = false;
        UIManager.Instance.OnDialogueEnded(); // Notifica al UIManager
    }
}