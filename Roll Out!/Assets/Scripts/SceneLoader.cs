using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string levelSelect = "Level Select";
    [SerializeField] private string endlessMode = "Endless Mode";
    [SerializeField] private string credits = "Credits";
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public AudioSource uiAudio;

    private void Start()
    {
        uiAudio = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) && FindObjectOfType<Player>().hasWon)
        {
            LoadNextLevel();
        }

        if (Input.GetButtonDown("Submit") && FindObjectOfType<Player>().hasWon)
        {
            LoadNextLevel();
        }

        if (Input.GetKey(KeyCode.R) && !FindObjectOfType<Player>().isAlive)
        {
            ReloadLevel();
        }

        if (Input.GetButtonDown("Cancel") && !FindObjectOfType<Player>().isAlive)
        {
            ReloadLevel();
        }
    }

    public void LevelSelectScreen()
    {
        uiAudio.Play();
        Time.timeScale = 1f;
        StartCoroutine(LevelSelectScreenTransition());
    }

    IEnumerator LevelSelectScreenTransition()
    {
        uiAudio.Play();
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelSelect);
    }
    
    public void MainMenu()
    {
        uiAudio.Play();
        Time.timeScale = 1f;
        StartCoroutine(MainMenuTransition());
    }

    IEnumerator MainMenuTransition()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        uiAudio.Play();
        Application.Quit();
    }

    public void EndlessMode()
    {
        uiAudio.Play();
        Time.timeScale = 1f;
        StartCoroutine(EndlessModeTransition());
    }

    IEnumerator EndlessModeTransition()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(endlessMode);
    }
    
    public void ReloadLevel()
    {
        uiAudio.Play();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        StartCoroutine(ReloadSceneTransition(currentSceneIndex));
    }

    public void ReloadLevelWhenFallen()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        StartCoroutine(ReloadSceneTransition(currentSceneIndex));
    }

    IEnumerator ReloadSceneTransition(int sceneIndex)
    {
        FindObjectOfType<Player>().dieMenu.SetActive(false);
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void LoadNextLevel()
    {
        uiAudio.Play();
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadNextLevelTransition(currentSceneIndex));
    }

    IEnumerator LoadNextLevelTransition(int sceneIndex)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void CreditsScreen()
    {
        uiAudio.Play();
        Time.timeScale = 1f;
        StartCoroutine(CreditsSceneTransition());
    }
    
    IEnumerator CreditsSceneTransition()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(credits);
    }
}
