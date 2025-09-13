using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Μεταβλητή για να ξέρουμε αν το παιχνίδι είναι σε παύση
    public static bool isPaused = false;

    // Σύρε εδώ από το Inspector το Panel του μενού σου
    public GameObject pauseMenuUI;

    // Σύρε εδώ την κάμερα του background
    public GameObject menuBackgroundCamera;
    public GameObject settingsMenuUI;


    void Update()
    {
        // Έλεγχος αν πατήθηκε το κουμπί Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Η συνάρτηση που καλείται από το κουμπί "Continue"
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        menuBackgroundCamera.SetActive(false);
        Time.timeScale = 1f; // Ξεπαγώνει τον χρόνο του παιχνιδιού
        isPaused = false;
        // Προαιρετικά: Ξεκλειδώνει τον κέρσορα αν τον είχες κλειδώσει
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Η συνάρτηση που παγώνει το παιχνίδι
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuBackgroundCamera.SetActive(true);
        Time.timeScale = 0f; // Παγώνει τον χρόνο του παιχνιδιού
        isPaused = true;
        // Προαιρετικά: Εμφανίζει τον κέρσορα
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
    }

    

    public void OpenSettings()
    {
        settingsMenuUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenuUI.SetActive(false);
    }
}