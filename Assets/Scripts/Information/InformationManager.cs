using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationManager : MonoBehaviour
{
    public static InformationManager Instance { get; set; }

    [Header("UI References")]
    public GameObject informationPopupUI;
    public TextMeshProUGUI popupTitleText;
    public TextMeshProUGUI popupDescriptionText;

    // ��� reference ��� �� Journal Controller
    [Header("Journal References")]
    public JournalUIController journalController;

    [Header("Animation Settings")]
    public float animationDuration = 0.5f;
    public float fadeInDuration = 0.3f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0f, 0.7f, 1f, 1f);
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Audio")]
    public AudioClip popupSound;
    private AudioSource audioSource;

    [Header("Settings")]
    public KeyCode journalToggleKey = KeyCode.C;

    private List<InformationData> collectedInformation = new List<InformationData>();
    private bool isJournalOpen = false;
    private Coroutine popupCoroutine;
    private CanvasGroup popupCanvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // ����������� ��� �� UI ����� ������� ���� ����
        informationPopupUI.SetActive(false);

        // ��������� �� Canvas Group component ��� animations
        popupCanvasGroup = informationPopupUI.GetComponent<CanvasGroup>();
        if (popupCanvasGroup == null)
        {
            popupCanvasGroup = informationPopupUI.AddComponent<CanvasGroup>();
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Debug.Log("InformationManager initialized successfully");
    }

    private void Update()
    {
        // Toggle journal �� �� 'C'
        if (Input.GetKeyDown(journalToggleKey))
        {
            ToggleJournal();
        }
    }

    public void CollectInformation(InformationData newInfo)
    {
        Debug.Log("Trying to collect information: " + newInfo.title);

        // ��������� �� ������ ��� ���� ��� ����������
        foreach (var info in collectedInformation)
        {
            if (info.title == newInfo.title)
            {
                Debug.Log("Information already exists");
                return; // ������ ��� ���� ��� ����������
            }
        }

        // ����������� �� ��� ����������
        newInfo.isCollected = true;
        collectedInformation.Add(newInfo);

        Debug.Log($"Information collected! Total: {collectedInformation.Count}");
    }

    public void ShowInformationPopup(InformationData info, float duration)
    {
        Debug.Log($"ShowInformationPopup called with title: {info.title}, duration: {duration}");

        // ������������ �� �������
        if (popupTitleText != null)
            popupTitleText.text = info.title;
        if (popupDescriptionText != null)
            popupDescriptionText.text = info.description;

        // ��������� ����������� ������ animation
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
            Debug.Log("Stopped previous popup coroutine");
        }

        // �������� ��� coroutine
        popupCoroutine = StartCoroutine(AnimatedPopupDisplay(duration));
    }

    private IEnumerator AnimatedPopupDisplay(float displayDuration)
    {
        // �������� ��� ���
        if (popupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(popupSound);
        }
        Debug.Log($"AnimatedPopupDisplay started - will show for {displayDuration} seconds");

        // ����������� �� popup
        informationPopupUI.SetActive(true);

        // ������� �����
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 0f;
        }
        informationPopupUI.transform.localScale = Vector3.one * 0.7f;

        // Fade in
        float fadeTime = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float progress = elapsedTime / fadeTime;

            if (popupCanvasGroup != null)
            {
                popupCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);
            }
            informationPopupUI.transform.localScale = Vector3.Lerp(Vector3.one * 0.7f, Vector3.one, progress);

            yield return null;
        }

        // ����������� ��� ������� �����
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 1f;
        }
        informationPopupUI.transform.localScale = Vector3.one;

        Debug.Log("Fade in complete");

        // ����������� ��� ����� ����� ���������
        yield return new WaitForSecondsRealtime(displayDuration);
        Debug.Log("Display duration complete");

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float progress = elapsedTime / fadeTime;

            if (popupCanvasGroup != null)
            {
                popupCanvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);
            }
            informationPopupUI.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.8f, progress);

            yield return null;
        }

        Debug.Log("Fade out complete");

        // �������� �� popup
        informationPopupUI.SetActive(false);

        // Reset ��� ��� ������� ����
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 1f;
        }
        informationPopupUI.transform.localScale = Vector3.one;

        Debug.Log("Popup hidden successfully");

        // ����������� �� coroutine reference
        popupCoroutine = null;
    }

    public void ToggleJournal()
    {
        Debug.Log("Toggle journal pressed. Count: " + collectedInformation.Count);
        isJournalOpen = !isJournalOpen;

        if (isJournalOpen)
        {
            // �������������� �� JournalUIController
            if (journalController != null)
            {
                journalController.ShowJournal();
                journalController.PopulateJournal(collectedInformation);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // �������� �� journal
            if (journalController != null)
            {
                journalController.HideJournal();
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public int GetCollectedInformationCount()
    {
        return collectedInformation.Count;
    }

    public List<InformationData> GetCollectedInformation()
    {
        return collectedInformation;
    }

    [ContextMenu("Force Hide Popup")]
    public void ForceHidePopup()
    {
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
            popupCoroutine = null;
        }
        informationPopupUI.SetActive(false);
        Debug.Log("Popup forcefully hidden");
    }

    [ContextMenu("Debug Component Status")]
    public void DebugComponentStatus()
    {
        Debug.Log($"InformationPopupUI: {(informationPopupUI != null ? "Found" : "NULL")}");
        Debug.Log($"PopupTitleText: {(popupTitleText != null ? "Found" : "NULL")}");
        Debug.Log($"PopupDescriptionText: {(popupDescriptionText != null ? "Found" : "NULL")}");
        Debug.Log($"PopupCanvasGroup: {(popupCanvasGroup != null ? "Found" : "NULL")}");
        Debug.Log($"PopupCoroutine: {(popupCoroutine != null ? "Running" : "NULL")}");
        Debug.Log($"Popup Active: {(informationPopupUI != null ? informationPopupUI.activeSelf : "NULL")}");
        Debug.Log($"Journal Controller: {(journalController != null ? "Found" : "NULL")}");
        Debug.Log($"Collected Information Count: {collectedInformation.Count}");
    }
}