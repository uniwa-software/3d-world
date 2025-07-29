using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }

    [Header("UI Refrences")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public GameObject continueButton;
    public Image speakerImage;

    [Header("Dialogue Settings")]
    public float typingSpeed = 0.05f;

    private Queue<string> sentences;
    private string activeSpeakerName;
    private Sprite activeSpeakerImage;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = null;
        }
    }

    private void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        // �� ������� Space � E ��� �������� �� �������, �������� ��� �� �������
        if (dialoguePanel.activeInHierarchy && isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            CompleteTyping();
        }
        // �� ������� Space � E ��� ���� ����������� �� typing, ������� ���� ������� �������
        else if (dialoguePanel.activeInHierarchy && !isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        // ������������ �� dialogue panel
        dialoguePanel.SetActive(true);

        // �������������� ��� ������ ��� ������
        if (InventorySystem.Instance != null)
        {
            // �������������� �� InventorySystem ��� �� ��������� �� � ������� ������ �� �������
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // ������� ��� �������� ��� ��� ������� ��� �������
        activeSpeakerName = dialogue.speakerName;
        activeSpeakerImage = dialogue.speakerImage;

        speakerText.text = activeSpeakerName;
        if (speakerImage != null && activeSpeakerImage != null)
        {
            speakerImage.sprite = activeSpeakerImage;
            speakerImage.gameObject.SetActive(true);
        }
        else
        {
            speakerImage.gameObject.SetActive(false);
        }

        // ���������� ������������ ���������
        sentences.Clear();

        // �������� ��� ���� ��������� ���� ����
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // �������� ��� ������ ��������
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // �� ��� �������� ����� ���������, �������� ��� �������
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // ���� ��� ������� �������
        string sentence = sentences.Dequeue();

        // ������� �� ����������� typing �� �������
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // ������ �� typing effect
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // �������� ��� �� ������� ��� ��������� ��������
        if (sentences.Count >= 0 && dialogueText.text.Length < dialogueText.text.Length)
        {
            dialogueText.text = dialogueText.text;
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        // �������������� �� dialogue panel
        dialoguePanel.SetActive(false);

        // ���������������� ��� ������ ��� ������
        if (InventorySystem.Instance != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // ����������
        dialogueText.text = "";
        sentences.Clear();
    }

    // ������� ��� �� Continue Button (�� ������ �� ��������������� button ���� ��� Space/E)
    public void OnContinueButton()
    {
        if (!isTyping)
        {
            DisplayNextSentence();
        }
        else
        {
            CompleteTyping();
        }
    }

}
