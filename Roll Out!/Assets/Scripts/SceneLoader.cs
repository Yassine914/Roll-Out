using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string levelSelect = "Level Select";
    [SerializeField] private string endlessMode = "Endless Mode";
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    

    public void LevelSelectScreen()
    {
        Time.timeScale = 1f;
        StartCoroutine(LevelSelectScreenTransition());
    }

    IEnumerator LevelSelectScreenTransition()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelSelect);
    }
    
    public void MainMenu()
    {
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
        Application.Quit();
    }

    public void EndlessMode()
    {
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
}
