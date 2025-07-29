using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Data")]
    public DialogueData dialogue;

    [Header("Interaction Settings")]
    public float interactionRange = 3f;
    public GameObject interactionPrompt; // UI ��� �� ������� "Press E to talk"
    public string promptText = "������ E ��� �� ��������";

    [Header("Visual Feedback")]
    public bool turnToPlayer = true;
    public float turnSpeed = 5f;

    private GameObject player;
    private bool playerInRange = false;
    private bool dialogueStarted = false;
    private Transform originalTransform;
    private Quaternion originalRotation;

    private void Start()
    {
        // ���� ��� ������
        player = GameObject.FindGameObjectWithTag("Player");

        // ����� �� interaction prompt ���� ����
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);

            // �� ���� Text component, ���� �� �������
            Text promptTextComponent = interactionPrompt.GetComponentInChildren<Text>();
            if (promptTextComponent != null)
            {
                promptTextComponent.text = promptText;
            }
        }

        // ���������� ��� ������ rotation
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // ������� ��������� ��� ��� ������
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= interactionRange && !dialogueStarted)
            {
                // � ������� ����� �����
                if (!playerInRange)
                {
                    playerInRange = true;
                    ShowInteractionPrompt();
                }

                // ������� ��� ������ ��� E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartDialogue();
                }
            }
            else if (distance > interactionRange && playerInRange)
            {
                // � ������� �������������
                playerInRange = false;
                HideInteractionPrompt();
            }

            // �� ������ �� ��� ������, ����� ���� �� ����� ���
            if (dialogueStarted && turnToPlayer)
            {
                TurnToPlayer();
            }
        }
    }

    private void ShowInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(true);
        }
    }

    private void HideInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        if (DialogueSystem.Instance != null && dialogue != null)
        {
            dialogueStarted = true;
            HideInteractionPrompt();
            DialogueSystem.Instance.StartDialogue(dialogue);

            // ������ coroutine ��� �� �������� ���� ��������� � ��������
            StartCoroutine(CheckDialogueEnd());
        }
    }

    private IEnumerator CheckDialogueEnd()
    {
        // �������� ����� �� ������� �� dialogue panel
        while (DialogueSystem.Instance.dialoguePanel.activeInHierarchy)
        {
            yield return null;
        }

        // � �������� ��������
        dialogueStarted = false;

        // ��������� ���� ������ rotation
        if (!turnToPlayer)
        {
            transform.rotation = originalRotation;
        }
    }

    private void TurnToPlayer()
    {
        if (player != null)
        {
            // ��������� ��� ���������� ���� ��� ������
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0; // ����� ���� ��� ��������� ����������

            // ��������� ��� rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ����� ����������
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    // ��� debugging - ������� �� range ��� Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}