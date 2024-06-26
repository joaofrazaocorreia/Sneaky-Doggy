using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject objectiveIndicator;
    public GameObject interactiveKeyIndicator;
    public GameObject arrowsCheckmark;
    public GameObject glowCheckmark;
    public GameObject smoothTurnCheckmark;
    public GameObject[] directionalArrows;
    public GameObject[] helpingGlows;

    [HideInInspector] public bool isPaused;
    [HideInInspector] public bool isTouchingInteractive;
    [HideInInspector] public bool smoothTurnEnabled;
    private bool arrowsEnabled;
    private bool glowEnabled;

    private void Start()
    {
        isPaused = pauseMenu.activeSelf;

        isTouchingInteractive = false;
        interactiveKeyIndicator.SetActive(false);

        arrowsEnabled = false;
        arrowsCheckmark.SetActive(false);

        glowEnabled = false;
        glowCheckmark.SetActive(false);

        smoothTurnEnabled = true;
        smoothTurnCheckmark.SetActive(true);



        if (PlayerPrefs.GetInt("Arrows", 0) == 0)
        {
            arrowsEnabled = false;
            arrowsCheckmark.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Glow", 0) == 0)
        {
            glowEnabled = false;
            glowCheckmark.SetActive(false);
        }

        if (PlayerPrefs.GetInt("SmoothTurn", 1) == 0)
        {
            smoothTurnEnabled = false;
            smoothTurnCheckmark.SetActive(false);
        }
        
        UpdateSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }


        if (isTouchingInteractive)
        {
            interactiveKeyIndicator.SetActive(true);
        }

        else if (interactiveKeyIndicator.activeSelf)
            interactiveKeyIndicator.SetActive(false);
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
    }

    public void GetObjective(GameObject objective)
    {
        objectiveIndicator.GetComponent<Image>().sprite = objective.GetComponentInChildren<SpriteRenderer>().sprite;
        objectiveIndicator.SetActive(true);
        
        Destroy(objective);
    }

    public void TogglePause()
    {
        settingsMenu.SetActive(false);

        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
    }

    public void ToggleSettingsMenu()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        pauseMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadHardmodeLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleDirectionalArrows()
    {
        arrowsEnabled = !arrowsEnabled;
        arrowsCheckmark.SetActive(arrowsEnabled);

        if (arrowsEnabled)
            PlayerPrefs.SetInt("Arrows", 1);
        else
            PlayerPrefs.SetInt("Arrows", 0);

            
        PlayerPrefs.Save();
        UpdateSettings();
    }

    public void ToggleHelpingGlow()
    {
        glowEnabled = !glowEnabled;
        glowCheckmark.SetActive(glowEnabled);

        if(glowEnabled)
            PlayerPrefs.SetInt("Glow", 1);
        else
            PlayerPrefs.SetInt("Glow", 0);

            
        PlayerPrefs.Save();
        UpdateSettings();
    }

    public void ToggleSmoothTurning()
    {
        smoothTurnEnabled = !smoothTurnEnabled;
        smoothTurnCheckmark.SetActive(smoothTurnEnabled);

        if(smoothTurnEnabled)
            PlayerPrefs.SetInt("SmoothTurn", 1);
        else
            PlayerPrefs.SetInt("SmoothTurn", 0);

            
        PlayerPrefs.Save();
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        foreach(GameObject go in directionalArrows)
        {
            go.SetActive(arrowsEnabled);
        }

        foreach(GameObject go in helpingGlows)
        {
            go.SetActive(glowEnabled);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
